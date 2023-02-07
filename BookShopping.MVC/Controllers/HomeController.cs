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
        private readonly IFeatureService _featureService;


        public HomeController(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        public IActionResult Index()
        {
            var features = _featureService.GetFeatures();

            return View(features);
        }
    }
}

