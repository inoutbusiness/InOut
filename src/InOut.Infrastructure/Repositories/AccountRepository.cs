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
        private Expression<Func<Account, bool>> ExpBySignInModel(string email, byte[] password)
            => w => w.Email.Equals(email) && w.Password.Equals(password);
        #endregion

        #region Public Methods
        public async Task<bool> ExistsByEmailAndPassword(string email, byte[] password)
        {
            return await _inOutContext.Accounts.AnyAsync(ExpBySignInModel(email, password));
        }

        public async Task<Account?> GetUserWithAccountByEmailAndPassword(string email, byte[] password)
        {
            return await _inOutContext.Accounts.Include(x => x.User).FirstOrDefaultAsync(ExpBySignInModel(email, password));
        }
        #endregion
    }
}