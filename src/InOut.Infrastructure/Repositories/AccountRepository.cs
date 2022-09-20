using InOut.Common;
using InOut.Domain.Entities;
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

        private Expression<Func<Account, bool>> ExpBySignInModel(string email, string password)
            => exp => exp.Email.Equals(email) && exp.Password.Equals(password);

        private Expression<Func<Account, bool>> ExpById(long accountId)
            => exp => exp.Id.Equals(accountId);

        #endregion

        public async Task<bool> ExistsByEmailAndPassword(string email, string password)
        {
            return await _inOutContext.Accounts
                                      .AnyAsync(ExpBySignInModel(email, password));
        }

        public async Task<Account?> GetUserWithAccountByEmailAndPassword(string email, string password)
        {
            return await _inOutContext.Accounts
                                      .Include(x => x.User)
                                      .FirstOrDefaultAsync(ExpBySignInModel(email, password));
        }

        public async Task ResetPassword(long accountId, string newPassword)
        {
            var account = await _inOutContext.Accounts.FirstOrDefaultAsync(ExpById(accountId));

            if (account != null)
            {
                _inOutContext.Entry(account).State = EntityState.Modified;
                account.Password = newPassword;
                await _inOutContext.SaveChangesAsync();
            }

            var a = await _inOutContext.Accounts.Where(x => x.Email == newPassword)
                                                .Select(s => s.Id)
                                                .SingleOrDefaultAsync();
        }

        public async Task<long> GetAccountIdByEmail(string email)
            => (await _inOutContext.Accounts.Where(x => x.Email == email)
                                            .Select(s => s.Id)
                                            .SingleOrDefaultAsync())
                                            .ToLong();
    }
}