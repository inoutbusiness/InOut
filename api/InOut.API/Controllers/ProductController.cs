using InOut.API.Builders;
using InOut.Domain.Exceptions;
using InOut.Domain.Models.Product;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;

        public ProductController(IProductService productService, IProductRepository productRepository)
        {
            _productService = productService;
            _productRepository = productRepository;
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

        [HttpGet]
        [Route("/api/v1/products/getAllProductsInfo")]
        public async Task<IActionResult> GetAllProductsInfo()
        {
            try
            {
                var products = _productRepository.GetAllAsNoTracking();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("Products not found: " + ex.Message)
                                                            .Build());
            }
        }
    }
}
