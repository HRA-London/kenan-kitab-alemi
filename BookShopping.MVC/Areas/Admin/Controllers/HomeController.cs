using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShopping.Domain.Enums;
using BookShopping.MVC.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShopping.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        // GET: /<controller>/
        [MyAuth(UserRoleEnum.Admin)]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Add()
        {
            return Content("add e geldi");
        }
    }
}

