using InOut.Domain.DTOs;
using InOut.Domain.Entities;
using InOut.Domain.Models.Brand;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;

namespace InOut.Service.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public BrandDto CreateBrand(BrandModel brandModel)
        {
            var newBrand = new Brand();
            newBrand.Name = brandModel.Name;
            newBrand.Logo = new byte[1];

            var brandCreated = _brandRepository.Create(newBrand);

            return new BrandDto()
            {
                Id = brandCreated.Id,
                Name = brandCreated.Name,
                Logo = brandCreated.Logo
            };
        }

        public IList<string> GetBrandNames()
        {
            return _brandRepository.GetAllBrandNames();
        }
    }
}
