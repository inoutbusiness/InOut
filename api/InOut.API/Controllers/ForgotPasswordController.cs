using InOut.API.Builders;
using InOut.API.Models;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InOut.API.Controllers
{
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public ForgotPasswordController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("/api/v1/forgotPassword/sendResetPasswordCode")]
        public async Task<IActionResult> SendEmailCodeResetPassword([FromBody] string emailRequest)
        {
            var url = "https://localhost:7145/api/v1/emailSender/sendEmail";

            try
            {
                using (var client = new HttpClient())
                {
                    var request = _accountService.CreateEmailSenderResetPasswordRequest(emailRequest);
                    var response = await client.PostAsJsonAsync(url, request);

                    var responseData = JsonConvert.DeserializeObject<EmailSenderResponse>(await response.Content.ReadAsStringAsync());
                    _configuration["EmailSenderWithCodeConfig:CodeConfig:Code"] = responseData?.Code;

                    return Ok(responseData);
                }
            }
            catch (HttpRequestException requestEx)
            {
                return StatusCode(404, new ResponseModelBuilder().WithMessage($"A url: {url} não foi reconhecida, verifique se a API está ONLINE."));
            }
            catch (Exception ex)
            {
                return StatusCode(401, new ResponseModelBuilder().WithMessage($"{ex.Message} {ex.InnerException?.Message}")
                                                                 .WithSuccess(false)
                                                                 .Build());
            }
        }

        [HttpPost]
        [Route("/api/v1/forgotPassword/emailCodeChecker")]
        public async Task<IActionResult> VerifyEmailCode([FromBody] string codeUserInput)
        {
            var codeReceived = _configuration["EmailSenderWithCodeConfig:CodeConfig:Code"].ToString();

            if (codeReceived == codeUserInput)
            {
                return Ok(new ResponseModel()
                {
                    Success = true,
                    Message = "Code checked!"
                });
            }

            return Ok(new ResponseModel());
        }

        [HttpPost]
        [Route("/api/v1/forgotPassword/ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            try
            {
                await _accountService.ResetPassword(resetPasswordModel.AccountId, resetPasswordModel.NewPassword);

                return Ok(new ResponseModel()
                {
                    Success = true,
                    Message = "Password changed"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(401, ex.Message);
            }
        }
    }
}
