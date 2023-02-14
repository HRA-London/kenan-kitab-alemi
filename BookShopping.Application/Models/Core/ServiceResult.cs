using System;
namespace BookShopping.Application.Models.Core
{
    public class ServiceResult<TResponse>
    {
        public TResponse Response { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public int StatusCode { get; set; }
    }
}

