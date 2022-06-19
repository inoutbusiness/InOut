using InOut.Domain.DTOs;
using InOut.Domain.Models.Auth;
using InOut.Domain.Models.User;

namespace InOut.Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> ExistsByEmailAndPassword(string email, byte[] password);
        Task<UserAccountModel> GetUserWithAccountByEmailAndPassword(string email, byte[] password);
        Task<UserDto> CreateAccountAndUserBySingUpModel(SignUpModel signUpModel);
    }
}