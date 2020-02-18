﻿
namespace Models
{
    public class User
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public long PhoneNumber { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string BankId { get; set; }

        public UserType UserType { get; set; }
    }
}
