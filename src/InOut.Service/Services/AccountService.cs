using EscNet.Cryptography.Interfaces;
using EscNet.Hashers.Interfaces.Algorithms;
using InOut.Common;
using AutoMapper;
using InOut.Domain.DTOs;
using InOut.Domain.Entities;
using InOut.Domain.Models.Auth;
using InOut.Domain.Models.User;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;
using System.Transactions;
using InOut.Domain.Exceptions;
using Microsoft.Extensions.Configuration;

namespace InOut.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IArgon2IdHasher _hasher;
        private readonly IRijndaelCryptography _rijndaelCryptography;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, IArgon2IdHasher hasher, 
                              IRijndaelCryptography rijndaelCryptography, IMapper mapper, IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _hasher = hasher;
            _rijndaelCryptography = rijndaelCryptography;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserAccountModel> GetUserWithAccountByEmailAndPassword(string email, string password)
        {
            var encryptedEmail = _rijndaelCryptography.Encrypt(email);
            var hashedPassword = _hasher.Hash(password);

            var account = await _accountRepository.GetUserWithAccountByEmailAndPassword(encryptedEmail, hashedPassword);

            if (account == null || account.User == null)
                throw new NotFoundedException("Conta ou Usuário não encontrado");

            return new UserAccountModel
            {
                Id = account.Id.ToLong(),
                FirstName = account.User.FirstName,
                LastName = account.User.LastName,
                BirthDate = account.User == null ? DateTime.MinValue : account.User.BirthDate,
                Phone = account.User.Phone,
                CpfCnpj = account.User.CpfCnpj,
                Email = account.Email,
            };
        }

        public async Task<UserDto> CreateAccountAndUser(SignUpModel signUpModel)
        {
            if (await _userRepository.ExistsByCpfCnpj(signUpModel.CpfCnpj))
                throw new AlreadyExistsException("Já existe um usuário com o CPF/CNPJ cadastrado");

            UserDto userDto;
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = new User
                {
                    BirthDate = signUpModel.BirthDate,
                    CpfCnpj = signUpModel.CpfCnpj,
                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName,
                    Phone = signUpModel.Phone,
                    Account = new Account
                    {
                        Email = _rijndaelCryptography.Encrypt(signUpModel.Email),
                        Password = _hasher.Hash(signUpModel.Password),
                    },
                    BranchId = signUpModel.BranchId,
                };

                var createdUser = await _userRepository.Create(user);
                tc.Complete();

                userDto = _mapper.Map<UserDto>(createdUser);
            }
            return userDto;
        }

        public object CreateEmailSenderResetPasswordRequest(string emailTo)
        {
            return new
            {
                Subject = "InOut - Redefinição de Senha",
                EmailFrom = _configuration["EmailSenderWithCodeConfig:EmailFrom"],
                EmailTo = emailTo,
                CodeConfig = new
                {
                    NumberDigits = _configuration["EmailSenderWithCodeConfig:CodeConfig:NumberDigits"],
                    ExpirationTime = _configuration["EmailSenderWithCodeConfig:CodeConfig:ExpirationTime"]
                },
                AuthenticateInfo = new
                {
                    EmailAuth = _configuration["EmailSenderWithCodeConfig:AuthenticateInfo:EmailAuth"],
                    PasswordAuth = _configuration["EmailSenderWithCodeConfig:AuthenticateInfo:PasswordAuth"]
                }
            };
        }  

        public async Task ResetPassword(long accountId, string newPassword)
        {
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _accountRepository.ResetPassword(accountId, _hasher.Hash(newPassword));
                tc.Complete();
            }
        }
    }
}