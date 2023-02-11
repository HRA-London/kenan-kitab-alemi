using System;
using BookShopping.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookShopping.MVC.Components
{
    public class HomeFeatureViewComponent : ViewComponent
    {
        private readonly IFeatureService _featureService;

        public HomeFeatureViewComponent(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var features = await _featureService.GetFeatures();
            return View(features);
        }
    }
}

