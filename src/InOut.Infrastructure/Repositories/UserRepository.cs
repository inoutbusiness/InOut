using InOut.Domain.Entities;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;

namespace InOut.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IInOutContext contextInOut) : base(contextInOut)
        {
        }
    }
}
