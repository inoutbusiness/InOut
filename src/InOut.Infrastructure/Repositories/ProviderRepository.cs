using InOut.Domain.Entities;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;

namespace InOut.Infrastructure.Repositories
{
    public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(IInOutContext inOutContext) : base(inOutContext)
        {
        }
    }
}