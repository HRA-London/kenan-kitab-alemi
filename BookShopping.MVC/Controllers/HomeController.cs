using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShopping.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BookShopping.MVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {

            return View();
        }
    }
}

