using System;
using System.Net.Http;
using System.Security.Claims;
using BookShopping.Application.Interfaces;
using BookShopping.Application.Models.Account.Login;
using BookShopping.Application.Models.Account.Register;
using BookShopping.Domain.Entities;
using BookShopping.Domain.Enums;
using BookShopping.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BookShopping.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContext _httpContext;
        public AccountService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users
                                         .Where(c => c.Email == request.Email)
                                             .FirstOrDefaultAsync();

            if (user == null)
                return null;


            var checkPasswordResult = user.CheckPassword(request.Password);


            if (!checkPasswordResult) return null;



            var claims = new List<Claim>
                 {
                     new Claim("name", user.Name),
                     new Claim("surname", user.Surname),
                     new Claim("id", user.Id.ToString()),
                 };

            var claimsIdentity = new ClaimsIdentity(
                claims, "Cookies");



            await _httpContext.SignInAsync(
                "Cookies",
                new ClaimsPrincipal(claimsIdentity));



            return new LoginResponse
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                UserId = user.Id
            };

        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var user = await _context
                                .Users
                                    .Where(c => c.Email == request.Email)
                                        .FirstOrDefaultAsync();


            if (user != null)
                return null;

            user = new User(request.Name,
                            request.Surname,
                            request.Email,
                            (int)UserStatusEnum.Waiting);


            user.MakePassword(request.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new RegisterResponse
            {
                UserId = user.Id
            };

        }


    }
}

