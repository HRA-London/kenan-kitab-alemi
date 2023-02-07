﻿using BookShopping.Application.Interfaces;
using BookShopping.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

app.UseStaticFiles();


app.Run();

