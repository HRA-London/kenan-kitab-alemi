using System;
namespace BookShopping.Domain.Entities
{
    public class UserRole : Entity<byte>
    {
        public string Name { get; set; }
    }
}

