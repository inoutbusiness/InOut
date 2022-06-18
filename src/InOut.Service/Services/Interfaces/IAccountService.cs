using InOut.Domain.Models.Auth;
using InOut.Domain.Models.User;

namespace InOut.Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> ExistsBySignInModel(SignInModel signInModel);
        Task<UserAccountModel> GetUserWithAccountBySignInModel(SignInModel signInModel);
    }
}