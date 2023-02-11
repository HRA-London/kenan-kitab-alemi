using System;
using BookShopping.Application.Models.Account.Login;
using BookShopping.Application.Models.Account.Register;

namespace BookShopping.Application.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    }
}

