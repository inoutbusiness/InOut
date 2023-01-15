using InOut.Domain.DTOs;
using InOut.Domain.Entities;

namespace InOut.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        IEnumerable<ProductNameWithCode> GetAllProductNamesWithCode();
        long GetProductIdByName(string productName);
        long GetProductIdByCode(string productCode);
    }
}