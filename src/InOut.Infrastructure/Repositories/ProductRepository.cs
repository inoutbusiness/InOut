using InOut.Common;
using InOut.Domain.DTOs;
using InOut.Domain.Entities;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;

namespace InOut.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly InOutContext _inOutContext;

        public ProductRepository(InOutContext inOutContext) : base(inOutContext)
        {
            _inOutContext = inOutContext;
        }

        public IEnumerable<ProductNameWithCode> GetAllProductNamesWithCode()
            => _inOutContext.Products
                            .Select(s => new ProductNameWithCode
                            {
                                Name = s.Name,
                                Code = s.Code
                            })
                            .Distinct()
                            .ToList();

        public long GetProductIdByName(string productName)
            => _inOutContext.Products
                            .Where(x => x.Name == productName)
                            .Select(s => s.Id)
                            .SingleOrDefault()
                            .ToLong();

        public long GetProductIdByCode(string productCode)
            => _inOutContext.Products
                            .Where(x => x.Code == productCode)
                            .Select(s => s.Id)
                            .SingleOrDefault()
                            .ToLong();
    }
}