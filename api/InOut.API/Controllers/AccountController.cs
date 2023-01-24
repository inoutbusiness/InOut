using EscNet.Cryptography.Interfaces;
using InOut.API.Builders;
using InOut.Common;
using InOut.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRijndaelCryptography _rijndaelCryptography;

        public AccountController(IAccountRepository accountRepository, IRijndaelCryptography rijndaelCryptography)
        {
            _accountRepository = accountRepository;
            _rijndaelCryptography = rijndaelCryptography;
        }

        [HttpPost]
        [Route("/api/v1/accounts/getAccountIdByEmail")]
        public async Task<IActionResult> GetAccountIdByEmail([FromBody] string email)
        {
            var accountId = await _accountRepository.GetAccountIdByEmail(_rijndaelCryptography.Encrypt(email));

            if (accountId.IsIdValid())
            {
                return Ok(new ResponseModelBuilder().WithMessage($"Account Id: {accountId}")
                                                    .WithSuccess(true)
                                                    .WithData(accountId)
                                                    .Build());
            }

            return BadRequest(new ResponseModelBuilder().WithMessage("Account Id not found")
                                                        .Build());
        }
    }
}
