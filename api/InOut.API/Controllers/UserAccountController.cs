using InOut.Domain.Models.User;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly IUserAccountService _userAccountService;

        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpPut]
        [Route("/api/v1/userAccount/updateUserAccountInfo/{accountId}")]
        public async Task<IActionResult> UpdateUserAccountInfo(int accountId, [FromBody] UserAccountModel userAccountModel)
        {
            try
            {
                userAccountModel.Id = accountId;

                var response = await _userAccountService.UpdateUserAccountInfo(userAccountModel);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
