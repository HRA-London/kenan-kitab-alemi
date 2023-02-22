using System;
using BookShopping.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShopping.MVC.Filters
{
    public class MyAuthAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly UserRoleEnum _roleEnum;
        public MyAuthAttribute(UserRoleEnum roleEnum)
        {
            _roleEnum = roleEnum;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var isAuthenticated = context.HttpContext.User?.Identity?.IsAuthenticated;

            if (isAuthenticated == null || isAuthenticated == false)
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            var roleId = context.HttpContext.User.Claims.Where(c => c.Type == "roleId").FirstOrDefault()?.Value;

            if (Convert.ToByte(roleId) != (byte)_roleEnum)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

        }
    }
}

