using System;
namespace BookShopping.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string body);
    }
}

