using System;
using BookShopping.Application.Models.Account.EmailValidation;
using BookShopping.Application.Models.Account.Login;
using BookShopping.Application.Models.Account.Register;
using BookShopping.Application.Models.Core;

namespace BookShopping.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest request);
        Task<ServiceResult<RegisterResponse>> RegisterAsync(RegisterRequest request);
        Task<ServiceResult<EmailValidationResponse>> CreateEmailValidationModel(Guid userId);
        Task<ServiceResult<bool>> ConfirmEmailAsync(string userId);
    }
}

