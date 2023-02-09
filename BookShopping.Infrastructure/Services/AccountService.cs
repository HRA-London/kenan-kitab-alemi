using System;
using BookShopping.Application.Interfaces;
using BookShopping.Application.Models.Account.Login;
using BookShopping.Application.Models.Account.Register;
using BookShopping.Infrastructure.Data;

namespace BookShopping.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }
        public LoginResponse Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

