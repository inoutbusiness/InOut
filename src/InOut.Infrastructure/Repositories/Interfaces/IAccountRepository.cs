using InOut.Domain.Entities;
using InOut.Domain.Models.Auth;

namespace InOut.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> ExistsByEmailAndPassword(string email, string password);
        Task<Account?> GetUserWithAccountByEmailAndPassword(string email, string password);
    }
}