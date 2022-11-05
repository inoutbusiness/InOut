using InOut.API.Builders;
using InOut.API.Models;
using InOut.Domain.Exceptions;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly IPasswordRecoveryService _passwordRecoveryService;

        public ForgotPasswordController(IAccountService accountService, IConfiguration configuration, IPasswordRecoveryService emailRecoveryService)
        {
            _accountService = accountService;
            _configuration = configuration;
            _passwordRecoveryService = emailRecoveryService;
        }

        /// <summary>
        /// Send a reset token to an expecified email.
        /// </summary>
        /// <param name="emailRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/v1/forgotPassword/sendResetPasswordCode")]
        public async Task<IActionResult> SendEmailCodeResetPassword([FromBody] string emailTo)
        {
            try
            {
                var responseData = await _passwordRecoveryService.SendRecoveryToken(emailTo);

                return Ok(responseData);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"Verifique se a API está ONLINE.")
                                                            .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage($"{ex.Message} {ex.InnerException?.Message}")
                                                            .Build());
            }
        }

        /// <summary>
        /// Validate if the inputed token is valid.
        /// </summary>
        /// <param name="verifyEmailCodeModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/v1/forgotPassword/emailCodeChecker")]
        public IActionResult VerifyEmailCode([FromBody] VerifyEmailCodeModel verifyEmailCodeModel)
        {
            try
            {
                _passwordRecoveryService.ValidateInputedToken(verifyEmailCodeModel.Email, verifyEmailCodeModel.InputedToken);

                return Ok(new ResponseModelBuilder().WithMessage("Código validado.")
                                                    .WithSuccess(true)
                                                    .Build());
            }
            catch (PasswordRecoveryException ex)
            {
                return BadRequest(new ResponseModelBuilder().WithMessage(ex.Message).Build());
            }
        }

        /// <summary>
        /// Set a new password to an expecified Account.
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/v1/forgotPassword/resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            try
            {
                await _accountService.ResetPassword(resetPasswordModel.AccountId, resetPasswordModel.NewPassword);

                return Ok(new ResponseModelBuilder().WithMessage("Senha alterada.")
                                                    .WithSuccess(true)
                                                    .Build());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}