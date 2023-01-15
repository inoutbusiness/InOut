using InOut.Domain.Exceptions;
using InOut.Domain.Models.Product;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    [ApiController]
    //[Route("/api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("/api/v1/products/registerProduct")]
        public IActionResult CreateProduct([FromBody] ProductModel productModel)
        {
            try
            {
                var productCreated = _productService.CreateProduct(productModel);
                return Ok(productCreated);

            }
            catch (AlreadyExistsException ex)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("/api/v1/products/getProductNamesWithCode")]
        public IActionResult GetProductNamesWithCode()
        {
            try
            {
                var productNamesWithCodes = _productService.GetProductNamesWithCode();
                return Ok(productNamesWithCodes);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
