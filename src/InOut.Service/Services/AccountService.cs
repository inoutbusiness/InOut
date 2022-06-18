using InOut.Domain.Models.Auth;
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
    }
}