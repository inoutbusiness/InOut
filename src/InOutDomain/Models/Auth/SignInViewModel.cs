﻿using System.ComponentModel.DataAnnotations;

namespace InOut.Domain.Models.Auth
{
    public class SignInModel
    {
        [Required(ErrorMessage = "O email não pode ser vázio.")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha não pode ser vázio.")]
        public string Password { get; set; } = string.Empty;
    }
}