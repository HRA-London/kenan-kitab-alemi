using BookShopping.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.ExpireTimeSpan = TimeSpan.FromHours(1);
                    option.Cookie.HttpOnly = true;
                    option.Cookie.IsEssential = true;
                    option.Cookie.SameSite = SameSiteMode.Lax;
                    option.SlidingExpiration = true;
                    option.LoginPath = "/account/auth";
                });

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

app.UseStaticFiles();

app.UseAuthentication();

app.UseSession();


app.Run();

