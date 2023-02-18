using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using BookShopping.Application.Interfaces;
using BookShopping.Application.Models.Account.EmailValidation;
using BookShopping.Application.Models.Account.Login;
using BookShopping.Application.Models.Account.Register;
using BookShopping.Application.Models.Core;
using BookShopping.Domain.Entities;
using BookShopping.Domain.Enums;
using BookShopping.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace BookShopping.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpContext _httpContext;
        private readonly IUrlHelper _urlHelper;
        public AccountService(ApplicationDbContext context,
                                IHttpContextAccessor httpContextAccessor,
                                IUrlHelperFactory urlHelperFactory,
                                 IActionContextAccessor actionContextAccessor
                                )
        {
            _context = context;
            _httpContext = httpContextAccessor.HttpContext;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public async Task<ServiceResult<bool>> ConfirmEmailAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id.ToString() == userId);

            //todo:user i yoxla
            if (user == null)
            {
                return new ServiceResult<bool>
                {
                    Errors = new Dictionary<string, string> { { "", "Bele bir istifadeci yoxdur." } },
                    Response = false,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }


            user.UserStatusId = (int)UserStatusEnum.Active;

            await _context.SaveChangesAsync();

            return new ServiceResult<bool>
            {
                Response = true,
                Errors = null,
                StatusCode = (int)HttpStatusCode.OK
            };

        }

        public async Task<ServiceResult<EmailValidationResponse>> CreateEmailValidationModel(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);

            if (user == null)
                return new ServiceResult<EmailValidationResponse>
                {
                    Response = null,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = new Dictionary<string, string> { { "", "Bele bir istifadeci tapilmadi." } }
                };

            if (user.UserStatusId == (int)UserStatusEnum.Active)
            {
                return new ServiceResult<EmailValidationResponse>
                {
                    Response = null,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = new Dictionary<string, string> { { "", "Bu istifadeci artiq tesdiq olunmusdur." } }
                };
            }


            var token = Guid.NewGuid().ToString();

            var link = _urlHelper.Action("Validate", "Account", new
            {
                token = token,
                userId = userId
            }, protocol: _httpContext.Request.Scheme);

            var body = $"<a href='{link}'>Aktiv etmek ucun ciqqildadin</a>";

            return new ServiceResult<EmailValidationResponse>
            {
                Response = new EmailValidationResponse
                {
                    Token = token,
                    URL = body
                },
                Errors = null,
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public async Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _context.Users
                                         .Where(c => c.Email == request.Email)
                                             .FirstOrDefaultAsync();

            if (user == null)
                return new ServiceResult<LoginResponse>
                {
                    Response = null,
                    Errors = new Dictionary<string, string>
                    {
                        {"","Bele bir istifadeci movcud deyil" }
                    },
                    StatusCode = (int)HttpStatusCode.BadRequest
                };


            var checkPasswordResult = user.CheckPassword(request.Password);


            if (!checkPasswordResult)
            {
                return new ServiceResult<LoginResponse>
                {
                    Response = null,
                    Errors = new Dictionary<string, string>
                    {
                        {"","Istifadeci adi ve ya sifre yanlisdir." }
                    },
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }


            if (user.UserStatusId == (int)UserStatusEnum.Waiting)
            {
                return new ServiceResult<LoginResponse>
                {
                    Response = null,
                    Errors = new Dictionary<string, string>
                    {
                        {"","Email tesdiq olunmayib." }
                    },
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

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



            return new ServiceResult<LoginResponse>
            {
                Response = new LoginResponse
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    UserId = user.Id
                },
                Errors = null,
                StatusCode = (int)HttpStatusCode.OK
            };

        }

        public async Task<ServiceResult<RegisterResponse>> RegisterAsync(RegisterRequest request)
        {
            var user = await _context
                                .Users
                                    .Where(c => c.Email == request.Email)
                                        .FirstOrDefaultAsync();


            if (user != null)
            {
                return new ServiceResult<RegisterResponse>
                {
                    Response = null,
                    Errors = new Dictionary<string, string>
                    {
                        { "","Bele bir istifadeci artiq qeydiyyatdan kecmisdir" }
                    },
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }


            user = new User(request.Name,
                            request.Surname,
                            request.Email,
                            (int)UserStatusEnum.Waiting);


            user.MakePassword(request.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ServiceResult<RegisterResponse>
            {
                Response = new RegisterResponse
                {
                    UserId = user.Id
                },
                Errors = null,
                StatusCode = (int)HttpStatusCode.OK
            };


        }


    }
}

