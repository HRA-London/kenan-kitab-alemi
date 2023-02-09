using System;
using BookShopping.Application.Models.Account.Login;
using BookShopping.Application.Models.Account.Register;

namespace BookShopping.MVC.Models.Account
{
    public class AuthModel
    {
        public LoginRequest LoginRequest { get; set; }
        public RegisterRequest RegisterRequest { get; set; }

    }
}

