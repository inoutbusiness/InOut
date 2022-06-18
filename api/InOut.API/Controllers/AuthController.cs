﻿using InOut.API.Builders;
using InOut.Domain.Models.Auth;
using InOut.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InOut.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("/api/v1/auth/signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInViewModel)
        {
            if (await _accountService.ExistsBySignInModel(signInViewModel))
            {
                return Ok(new ResponseModelBuilder().WithMessage("User exists!")
                                                    .WithSuccess(true)
                                                    .Build());
            }

            return StatusCode(401, new ResponseModelBuilder().WithMessage("User don't exists!")
                                                             .WithSuccess(false)
                                                             .Build());
        }
    }
}