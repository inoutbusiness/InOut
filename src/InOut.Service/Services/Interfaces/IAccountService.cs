﻿using InOut.Domain.DTOs;
using InOut.Domain.Models.Auth;
using InOut.Domain.Models.User;

namespace InOut.Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserAccountModel> GetUserWithAccountByEmailAndPassword(string email, string password);

        Task<UserDto> CreateAccountAndUser(SignUpModel signUpModel);

        Task ResetPassword(long accountId, string newPassword);

        Task<UserAccountModel> UpdateUserAccountInfo(UserAccountModel userAccountModel);
    }
}