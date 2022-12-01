using InOut.Domain.DTOs;
using InOut.Domain.Exceptions;
using InOut.Domain.Queues.Publishers;
using InOut.Domain.Queues.Publishers.Interfaces;
using InOut.Service.Cache.Interfaces;
using InOut.Service.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace InOut.Service.Services
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const short TOKEN_SIZE = 6;

        private readonly ICacheManager _cacheManager;
        private readonly IEmailSenderPublisher _emailSenderPublisher;

        public PasswordRecoveryService(ICacheManager cacheManager, IEmailSenderPublisher emailSenderPublisher)
        {
            _cacheManager = cacheManager;
            _emailSenderPublisher = emailSenderPublisher;
        }

        #region Public Methods

        public void SendRecoveryToken(string emailTo) 
        {
            var request = CreateRecoveryTokenRequest(emailTo);
            
            _emailSenderPublisher.Publish(JsonConvert.SerializeObject(request));
        }

        public void ValidateInputedToken(string email, string inputedToken)
        {
            var tokenInCache = _cacheManager.Get(email);

            if (string.IsNullOrEmpty(tokenInCache))
            {
                throw new PasswordRecoveryException($"Não foi possível recuperar um token de recuperação de senha para o email {email}.");
            }

            if (!tokenInCache.Equals(inputedToken))
            {
                throw new PasswordRecoveryException($"O token para recuperação de senha está incorreto.");
            }
        }

        public void RemoveFromCacheByEmail(string email)
        {
            _cacheManager.Remove(email);
        }

        #endregion Public Methods

        #region Private Methods

        private EmailSenderRequest CreateRecoveryTokenRequest(string emailTo)
        {
            var token = GenerateRecoveryToken();

            _cacheManager.Add(emailTo, token, 3600);

            return new EmailSenderRequest
            {
                Subject = "InOut - Redefinição de Senha",
                EmailFrom = "inout.empresa@gmail.com",
                EmailTo = emailTo,
                RecoveryToken = token,
                AuthenticateInfo = new EmailAuthenticateInfoDto
                {
                    EmailAuth = "inout.empresa@gmail.com",
                    PasswordAuth = "tzooaunmwuqfhlcr"
                }
            };
        }

        private string GenerateRecoveryToken()
        {
            var random = new Random();
            return new string(Enumerable.Repeat(CHARS, TOKEN_SIZE).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion Private Methods
    }
}