using InOut.Domain.Models.Auth;

namespace InOut.Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> ExistsBySignInModel(SignInModel signInModel);
    }
}