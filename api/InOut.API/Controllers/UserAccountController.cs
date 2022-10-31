using EscNet.Cryptography.Interfaces;
using InOut.Common;
using InOut.Domain.Models.User;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IAccountService _accountService;

        public UserAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPut]
        [Route("/api/v1/userAccount/updateUserAccountInfo/{accountId}")]
        public async Task<IActionResult> UpdateUserAccountInfo(int accountId, [FromBody] UserAccountModel userAccountModel)
        {
            try
            {
                userAccountModel.Id = accountId;

                var response = await _accountService.UpdateUserAccountInfo(userAccountModel);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
