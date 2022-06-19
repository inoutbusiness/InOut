using InOut.Domain.Entities;
using InOut.Domain.Models.Auth;

namespace InOut.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> ExistsByEmailAndPassword(string email, byte[] password);
        Task<Account?> GetUserWithAccountByEmailAndPassword(string email, byte[] password);
    }
}