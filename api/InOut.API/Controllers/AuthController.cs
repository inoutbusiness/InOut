using InOut.API.Builders;
using InOut.Domain.Enums;
using InOut.Domain.Interfaces;
using InOut.Domain.Models.Auth;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICrypt _crypt;
        private readonly IUserService _userService;

        public AuthController(IAccountService accountService, ICrypt crypt, IUserService userService)
        {
            _accountService = accountService;
            _crypt = crypt;
            _userService = userService;
        }

        #region Public Methods
        [HttpPost]
        [Route("/api/v1/auth/signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            try
            {
                var hashedPassword = _crypt.Encrypt(signInModel.Password, EEncryptionType.Password);
                if (await _accountService.ExistsByEmailAndPassword(signInModel.Email, hashedPassword))
                {
                    var userAccountModel = await _accountService.GetUserWithAccountByEmailAndPassword(signInModel.Email, hashedPassword);
                    return Ok(new ResponseModelBuilder().WithMessage("User exists!")
                                                        .WithSuccess(true)
                                                        .WithData(userAccountModel)
                                                        .Build());
                }

                return StatusCode(401, new ResponseModelBuilder().WithMessage("User don't exists!")
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
                if (await CanCreateUser(signUpModel))
                {
                    var createdUser = await _accountService.CreateAccountAndUserBySingUpModel(signUpModel);
                    return Created("/api/v1/auth/signup", new ResponseModelBuilder().WithMessage("User Created!")
                                                                                    .WithSuccess(true)
                                                                                    .WithData(createdUser)
                                                                                    .Build());
                }

                return StatusCode(401, new ResponseModelBuilder().WithMessage("User already exists!")
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
        #endregion

        #region Private Methods
        private async Task<bool> CanCreateUser(SignUpModel signUpModel)
        {
            var hashedPassword = _crypt.Encrypt(signUpModel.Password, EEncryptionType.Password);
            return !await _accountService.ExistsByEmailAndPassword(signUpModel.Email, hashedPassword) && 
                   !await _userService.ExistsByCpfCnpj(signUpModel.CpfCnpj);
        }
        #endregion
    }
}