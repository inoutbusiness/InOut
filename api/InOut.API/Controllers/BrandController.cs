using InOut.API.Builders;
using InOut.Domain.Exceptions;
using InOut.Domain.Models.Brand;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandService brandService, IBrandRepository brandRepository)
        {
            _brandService = brandService;
            _brandRepository = brandRepository;
        }

        [HttpPost]
        [Route("/api/v1/brands/registerBrand")]
        public IActionResult CreateBrand([FromBody] BrandModel brandModel)
        {
            try
            {
                var brandCreated = _brandService.CreateBrand(brandModel);

                return Ok(brandCreated);
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/v1/brands/getBrandNames")]
        public IActionResult GetBrandNames()
        {
            try
            {
                var brandNames = _brandService.GetBrandNames();
                return Ok(brandNames);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("/api/v1/brands/getAllBrandsInfo")]
        public async Task<IActionResult> GetAllBrandInfo()
        {
            try
            {
                var brands = _brandRepository.GetAllAsNoTracking();

                return Ok(brands);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("Brands not found: " + ex.Message)
                                                            .Build());
            }
        }
    }
}
