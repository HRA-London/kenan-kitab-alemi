﻿using BookShopping.Application.Interfaces;
using BookShopping.Infrastructure.Data;
using BookShopping.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.Infrastructure
{

    //Method Extensions
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration["Database"],
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


            services.AddTransient<IFeatureService, FeatureService>();

            return services;
        }
    }
}