using InOut.API.Builders;
using InOut.Domain.Models.Auth;
using InOut.Service.Services.Interfaces;
using InOut.Service.Token.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace InOut.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IAccountService accountService, IUserService userService, ITokenGenerator tokenGenerator)
        {
            _accountService = accountService;
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }

        #region Public Methods
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
        #endregion

        #region Private Methods
        private async Task<bool> CanCreateUser(SignUpModel signUpModel)
        {
            //var hashedPassword = _crypt.Encrypt(signUpModel.Password, EEncryptionType.Password); vai ser implementado o hash de senha

            return !await _accountService.ExistsByEmailAndPassword(signUpModel.Email, signUpModel.Password) && 
                   !await _userService.ExistsByCpfCnpj(signUpModel.CpfCnpj);
        }

        #endregion

        [HttpGet]
        [Route("/api/v1/emailGetter/getEmail")]
        public async Task<IActionResult> GetEmailMicroServiceTest()
        {
            try
            {
                var url = "https://localhost:7145/emailSender/api/v1/sendEmail";
                var client = new HttpClient();

                client.BaseAddress = new Uri(url);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(url);

                var data = response.Content.ReadAsStringAsync().Result;

                client.Dispose();

                return Ok();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}