using InOut.Domain.Models.Auth;
using InOut.Domain.Models.User;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;

namespace InOut.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> ExistsBySignInModel(SignInModel signInModel)
        {
            return await _accountRepository.ExistsBySignInModel(signInModel);
        }

        public async Task<UserAccountModel> GetUserWithAccountBySignInModel(SignInModel signInModel)
        {
            var account = await _accountRepository.GetUserWithAccountBySignInModel(signInModel);

            return new UserAccountModel
            {
                FirstName = account.User?.FirstName,
                LastName = account.User?.LastName,
                BirthDate = account.User == null ? DateTime.MinValue : account.User.BirthDate,
                Phone = account.User?.Phone,
                CpfCnpj = account.User?.CpfCnpj,
                Email = account.Email,
            };
        }
    }
}