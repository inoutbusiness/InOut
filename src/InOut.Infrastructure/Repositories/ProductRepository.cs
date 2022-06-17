using InOut.Domain.Entities;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;

namespace InOut.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IInOutContext contextInOut) : base(contextInOut)
        {
        }
    }
}
