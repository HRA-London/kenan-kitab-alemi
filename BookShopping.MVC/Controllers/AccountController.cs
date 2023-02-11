using System;
using BookShopping.Application.Interfaces;
using BookShopping.MVC.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BookShopping.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Auth()
        {
            return View(new AuthModel());
        }


        [HttpPost]
        public async Task<IActionResult> Auth(AuthModel model)
        {
            if (model.RegisterRequest != null)
            {
                var registerResponse = await _accountService.RegisterAsync(model.RegisterRequest);

                return RedirectToAction("Auth", "Account");
                //TODO: Send Email
            }
            else if (model.LoginRequest != null)
            {
                var loginResponse = await _accountService.LoginAsync(model.LoginRequest);

                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Index", "Home");
        }
    }
}

