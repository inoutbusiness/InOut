using EscNet.Cryptography.Interfaces;
using InOut.API.Builders;
using InOut.Common;
using InOut.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    // Esta classe deve ter o nome alterado ou até mesmo ser excluida, vamos ver um padrão melhor
    // para fazer essas classes API que auxiliam o front em React

    [ApiController]
    public class HelpController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRijndaelCryptography _rijndaelCryptography;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;

        public HelpController(IAccountRepository accountRepository, IRijndaelCryptography rijndaelCryptography, IUserRepository userRepository, IProductRepository productRepository, IBrandRepository brandRepository)
        {
            _accountRepository = accountRepository;
            _rijndaelCryptography = rijndaelCryptography;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
        }

        [HttpPost]
        [Route("/api/v1/helper/getAccountIdByEmail")] //trocar nome talvez?
        public async Task<IActionResult> GetAccountIdByEmail([FromBody] string email)
        {
            var accountId = await _accountRepository.GetAccountIdByEmail(_rijndaelCryptography.Encrypt(email));

            if (accountId.IsIdValid())
            {
                return Ok(new ResponseModelBuilder().WithMessage($"Account Id: {accountId}")
                                                    .WithSuccess(true)
                                                    .WithData(accountId)
                                                    .Build());                      // ALTERAR NO REACT TAMBEM, A CHAMADA MUDA
            }

            return BadRequest(new ResponseModelBuilder().WithMessage("Account Id not found")
                                                        .Build());
        }

        // METODO ABAIXO FEITO SOMENTE PARA TESTES
        [HttpGet]
        [Route("/api/v1/helper/getAllUsersInfo")]
        public async Task<IActionResult> GetAllUsersInfo()
        {
            try
            {
                var users = _userRepository.GetAllAsNoTracking();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("Users not found")
                                                            .Build());
            }
        }

        [HttpGet]
        [Route("/api/v1/helper/getAllProductsInfo")]
        public async Task<IActionResult> GetAllProductsInfo()
        {
            try
            {
                var products = _productRepository.GetAllAsNoTracking();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("Products not found")
                                                            .Build());
            }
        }

        [HttpGet]
        [Route("/api/v1/helper/getAllBrandsInfo")]
        public async Task<IActionResult> GetAllBrandInfo()
        {
            try
            {
                var brands = _brandRepository.GetAllAsNoTracking();

                return Ok(brands);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage("Brands not found")
                                                            .Build());
            }
        }
    }
}