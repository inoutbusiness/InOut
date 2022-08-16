using InOut.Domain.Entities;

namespace InOut.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> ExistsByEmailAndPassword(string email, string password);
        Task<Account?> GetUserWithAccountByEmailAndPassword(string email, string password);
        Task ResetPassword(long accountId, string newPassword);
        Task<long> GetAccountIdByEmail(string email);
    }
}