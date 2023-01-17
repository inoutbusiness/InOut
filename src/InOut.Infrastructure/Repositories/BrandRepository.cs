using InOut.Common;
using InOut.Domain.Entities;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;

namespace InOut.Infrastructure.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        private readonly InOutContext _inOutContext;

        public BrandRepository(InOutContext inOutContext) : base(inOutContext)
        {
            _inOutContext = inOutContext;
        }

        public IList<string> GetAllBrandNames()
            => _inOutContext.Brands
                            .Select(b => b.Name)
                            .ToList();

        public long GetBrandIdByName(string brandName)
            => _inOutContext.Brands
                            .Where(x => x.Name == brandName)
                            .Select(s => s.Id)
                            .SingleOrDefault()
                            .ToLong();
    }
}
