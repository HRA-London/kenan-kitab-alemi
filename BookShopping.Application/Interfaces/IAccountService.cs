using System;
using BookShopping.Application.Models.Account.Login;
using BookShopping.Application.Models.Account.Register;

namespace BookShopping.Application.Interfaces
{
    public interface IAccountService
    {
        LoginResponse Login(LoginRequest request);
        RegisterResponse Register(RegisterRequest request);
    }
}

