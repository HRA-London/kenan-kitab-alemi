using System;
namespace BookShopping.Domain.Entities
{
    public class UserAddress : Entity<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public User User { get; set; }

    }
}

