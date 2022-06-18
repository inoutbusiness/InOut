using InOut.Domain.Entities;
using InOut.Domain.Models.Auth;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InOut.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly IInOutContext _inOutContext;

        public AccountRepository(IInOutContext inOutContext) : base(inOutContext)
        {
            _inOutContext = inOutContext;
        }

        private Expression<Func<Account, bool>> ExpExistsBySignInModel(SignInModel signInModel)
            => w => w.Email.Equals(signInModel.Email) && w.Password.Equals(signInModel.Password);

        public async Task<bool> ExistsBySignInModel(SignInModel signInModel)
        {
            return await _inOutContext.Accounts.AnyAsync(ExpExistsBySignInModel(signInModel));
        }
    }
}