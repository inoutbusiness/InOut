using InOut.API.Builders;
using InOut.Domain.Enums;
using InOut.Domain.Interfaces;
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
        private readonly ICrypt _crypt;
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IAccountService accountService, ICrypt crypt, IUserService userService, ITokenGenerator tokenGenerator)
        {
            _accountService = accountService;
            _crypt = crypt;
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpGet]
        [Route("/api/v1/auth/gettoken")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken([FromBody] SignInModel test)
        {
            try
            {
                var token = _tokenGenerator.GenerateToken(test.Email);

                return Ok(new ResponseModelBuilder().WithMessage("Test")
                                                    .WithSuccess(true)
                                                    .WithData(new
                                                    {
                                                        Token = token
                                                    })
                                                    .Build());
                                                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        [Route("/api/v1/auth/signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            try
            {
                //var hashedPassword = _crypt.Encrypt(signInModel.Password, EEncryptionType.Password); vai ser implementado o hash de senha

                if (await _accountService.ExistsByEmailAndPassword(signInModel.Email, signInModel.Password))
                {
                    var userAccountModel = await _accountService.GetUserWithAccountByEmailAndPassword(signInModel.Email, signInModel.Password);

                    return Ok(new ResponseModelBuilder().WithMessage("User exists!")
                                                        .WithSuccess(true)
                                                        .WithData(new
                                                        {
                                                            Token = _tokenGenerator.GenerateToken(userAccountModel.Email),
                                                            TokenExpires = 1, // colocar isso parametrizado dentro do sistema
                                                            Data = userAccountModel
                                                        })
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

        #region Private Methods
        private async Task<bool> CanCreateUser(SignUpModel signUpModel)
        {
            //var hashedPassword = _crypt.Encrypt(signUpModel.Password, EEncryptionType.Password); vai ser implementado o hash de senha

            return !await _accountService.ExistsByEmailAndPassword(signUpModel.Email, signUpModel.Password) && 
                   !await _userService.ExistsByCpfCnpj(signUpModel.CpfCnpj);
        }
        #endregion
    }
}