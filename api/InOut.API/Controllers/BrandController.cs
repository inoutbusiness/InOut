using InOut.Domain.Exceptions;
using InOut.Domain.Models.Brand;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    [ApiController]
    //[Route("/api/v1/brands")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
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
    }
}
