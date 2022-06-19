using InOut.Domain.Entities;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InOut.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly InOutContext _inOutContext;
        public UserRepository(InOutContext inOutContext) : base(inOutContext)
        {
            _inOutContext = inOutContext;
        }

        private Expression<Func<User, bool>> ExpExistsByCpfCnpj(string cpfCnpj)
            => w => w.CpfCnpj.Equals(cpfCnpj);

        public async Task<bool> ExistsByCpfCnpj(string cpfCnpj)
        {
            return await _inOutContext.Users.AnyAsync(ExpExistsByCpfCnpj(cpfCnpj));
        }
    }
}