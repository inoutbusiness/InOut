using InOut.Domain.DTOs;

namespace InOut.Service.Services.Interfaces
{
    public interface IPasswordRecoveryService
    {
        Task<EmailSenderDto?> SendRecoveryToken(string emailTo);

        void ValidateInputedToken(string email, string inputedToken);

        void RemoveFromCacheByEmail(string email);
    }
}