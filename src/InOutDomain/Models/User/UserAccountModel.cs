﻿namespace InOut.Domain.Models.User
{
    public class UserAccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CpfCnpj { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
    }
}
