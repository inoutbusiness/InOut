using InOut.Domain.Entities;
using InOut.Domain.Models.Auth;
using InOut.Domain.Models.User;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InOut.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly InOutContext _inOutContext;

        public AccountRepository(InOutContext inOutContext) 
            : base(inOutContext)
        {
            _inOutContext = inOutContext;
        }

        #region Expressions
        private Expression<Func<Account, bool>> ExpBySignInModel(SignInModel signInModel)
            => w => w.Email.Equals(signInModel.Email) && w.Password.Equals(signInModel.Password);
        #endregion

        #region Public Methods
        public async Task<bool> ExistsBySignInModel(SignInModel signInModel)
        {
            return await _inOutContext.Accounts.AnyAsync(ExpBySignInModel(signInModel));
        }

        public async Task<Account> GetUserWithAccountBySignInModel(SignInModel signInModel)
        {
            return await _inOutContext.Accounts
                                        .Include(x => x.User)
                                            .FirstOrDefaultAsync(ExpBySignInModel(signInModel));
        }
        #endregion
    }
}