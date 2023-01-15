using InOut.Domain.DTOs;
using InOut.Domain.Models.Product;

namespace InOut.Service.Services.Interfaces
{
    public interface IProductService
    {
        ProductDto CreateProduct(ProductModel productModel);
        IEnumerable<ProductNameWithCode> GetProductNamesWithCode();
        string BuildProductSKUCode(string productName, string productType);
    }
}