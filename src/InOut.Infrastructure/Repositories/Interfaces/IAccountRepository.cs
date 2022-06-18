using InOut.Domain.Entities;
using InOut.Domain.Models.Auth;

namespace InOut.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> ExistsBySignInModel(SignInModel signInModel);
        Task<Account> GetUserWithAccountBySignInModel(SignInModel signInModel);
    }
}