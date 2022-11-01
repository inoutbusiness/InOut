using InOut.Domain.Models.User;

namespace InOut.Service.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task<UserAccountModel> UpdateUserAccountInfo(UserAccountModel userAccountModel);
    }
}
