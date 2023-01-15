using InOut.Domain.Entities;

namespace InOut.Infrastructure.Repositories.Interfaces
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        IList<string> GetAllBrandNames();
        long GetBrandIdByName(string brandName);
    }
}
