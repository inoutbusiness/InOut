using InOut.Domain.DTOs;
using InOut.Domain.Models.Brand;

namespace InOut.Service.Services.Interfaces
{
    public interface IBrandService
    {
        BrandDto CreateBrand(BrandModel brandModel);
        IList<string> GetBrandNames();
    }
}
