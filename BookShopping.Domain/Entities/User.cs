using System;
namespace BookShopping.Domain.Entities
{
    public class User : Entity<Guid>
    {

        public User()
        {
            Addresses = new HashSet<UserAddress>();
        }


        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public int UserStatusId { get; set; }

        public ICollection<UserAddress> Addresses { get; set; }

    }
}

