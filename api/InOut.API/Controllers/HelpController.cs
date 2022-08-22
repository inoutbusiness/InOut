using EscNet.Cryptography.Interfaces;
using InOut.API.Models;
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

        public HelpController(IAccountRepository accountRepository, IRijndaelCryptography rijndaelCryptography)
        {
            _accountRepository = accountRepository;
            _rijndaelCryptography = rijndaelCryptography;
        }

        [HttpPost]
        [Route("/api/v1/forgotPassword/getAccountId")]
        public async Task<IActionResult> GetAccountIdByEmail([FromBody] string email)
        {
            var accountId = await _accountRepository.GetAccountIdByEmail(_rijndaelCryptography.Encrypt(email));

            return Ok(
            new ResponseModel()
            {
                Success = true,
                Data = accountId.ToString()
            });
        }
    }
}
