using System;
using BookShopping.MVC.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace BookShopping.MVC.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Auth()
        {
            return View(new AuthModel());
        }


        [HttpPost]
        public IActionResult Auth(AuthModel model)
        {
            return View();
        }
    }
}

