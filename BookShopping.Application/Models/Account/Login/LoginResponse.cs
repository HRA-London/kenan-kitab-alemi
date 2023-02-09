using System;
namespace BookShopping.Application.Models.Account.Login
{
    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}

