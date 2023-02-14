using System;
using System.Net;
using BookShopping.Application.Interfaces;
using BookShopping.Domain.DTOs;
using BookShopping.Domain.Entities;
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
            var result = await _featureService.GetFeatures();

            if (result.StatusCode == (int)HttpStatusCode.OK)
                return View(result.Response);

            return View(new List<FeatureDto>());
        }
    }
}

