using BookShopping.Application.Interfaces;
using BookShopping.Infrastructure.Data;
using BookShopping.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration["Database"],
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddHttpContextAccessor();

            services.AddTransient<ITransactionHandler, TransactionHandler>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IFeatureService, FeatureService>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }
    }
}
