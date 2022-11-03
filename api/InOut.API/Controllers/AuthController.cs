using InOut.API.Builders;
using InOut.API.Models.ResponsesDTOs;
using InOut.Domain.Exceptions;
using InOut.Domain.Models.Auth;
using InOut.Service.Services.Interfaces;
using InOut.Service.Token.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            try
            {
                var userAccountModelCretated = await _accountService.GetUserWithAccountByEmailAndPassword(signInModel.Email, signInModel.Password);
                var generatedToken = _tokenGenerator.GenerateToken(userAccountModelCretated.Id);

                return Ok(new ResponseModelBuilder().WithMessage("User exists!")
                                                    .WithSuccess(true)
                                                    .WithData(new SigninResponseDto()
                                                    {
                                                        UserAccountModel = userAccountModelCretated,
                                                        TokenData = generatedToken
                                                    })
                                                    .Build());
            }
            catch (NotFoundedException ex)
            {
                return NotFound(new ResponseModelBuilder().WithMessage(ex.Message)
                                                          .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"{ex.Message} {ex.InnerException?.Message}")
                                                            .Build());
            }
        }

        [HttpPost]
        [Route("/api/v1/auth/signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            try
            {
                var createdUser = await _accountService.CreateAccountAndUser(signUpModel);

                return Created("/api/v1/auth/signup", new ResponseModelBuilder().WithMessage("User Created!")
                                                                                .WithSuccess(true)
                                                                                .WithData(createdUser)
                                                                                .Build());
            }
            catch (AlreadyExistsException ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message)
                                                            .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"{ex.Message} {ex.InnerException?.Message}")
                                                            .Build());
            }
        }
    }
}