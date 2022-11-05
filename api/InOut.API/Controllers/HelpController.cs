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

        public HelpController(IAccountRepository accountRepository, IRijndaelCryptography rijndaelCryptography)
        {
            _accountRepository = accountRepository;
            _rijndaelCryptography = rijndaelCryptography;
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
    }
}