using InOut.API.Builders;
using InOut.Domain.Exceptions;
using InOut.Domain.Models.Auth;
using InOut.Service.Services.Interfaces;
using InOut.Service.Token.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IAccountService accountService, ITokenGenerator tokenGenerator)
        {
            _accountService = accountService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        [Route("/api/v1/auth/signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            try
            {
                var userAccountModel = await _accountService.GetUserWithAccountByEmailAndPassword(signInModel.Email, signInModel.Password);

                return Ok(new ResponseModelBuilder().WithMessage("User exists!")
                                                    .WithSuccess(true)
                                                    .WithData(_tokenGenerator.GenerateToken(userAccountModel.Id))
                                                    .Build());
            }
            catch (NotFoundedException ex)
            {
                return StatusCode(401, new ResponseModelBuilder().WithMessage(ex.Message)
                                                                 .WithSuccess(false)
                                                                 .Build());
            }
            catch (Exception ex)
            {
                return StatusCode(401, new ResponseModelBuilder().WithMessage($"{ex.Message} {ex.InnerException?.Message}")
                                                                 .WithSuccess(false)
                                                                 .Build());
            }
        }

        [HttpPost]
        [Route("/api/v1/auth/signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            try
            {
                var createdUser = await _accountService.CreateAccountAndUserBySingUpModel(signUpModel);

                return Created("/api/v1/auth/signup", new ResponseModelBuilder().WithMessage("User Created!")
                                                                                .WithSuccess(true)
                                                                                .WithData(createdUser)
                                                                                .Build());
            }
            catch (AlreadyExistsException ex)
            {
                return StatusCode(401, new ResponseModelBuilder().WithMessage(ex.Message)
                                                                 .WithSuccess(false)
                                                                 .Build());
            }
            catch (Exception ex)
            {
                return StatusCode(401, new ResponseModelBuilder().WithMessage($"{ex.Message} {ex.InnerException?.Message}")
                                                                 .WithSuccess(false)
                                                                 .Build());
            }
        }
    }
}