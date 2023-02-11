using System;
using System.Security.Cryptography;
using System.Text;

namespace BookShopping.Domain.Entities
{
    public class User : Entity<Guid>
    {

        public User()
        {
            Addresses = new HashSet<UserAddress>();
        }

        public User(string name, string surname, string email, int statusId)
        {
            Name = name;
            Surname = surname;
            Email = email;
            UserStatusId = statusId;

        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public int UserStatusId { get; set; }

        public ICollection<UserAddress> Addresses { get; set; }


        public void MakePassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(password);

                var hash = sha256.ComputeHash(buffer);

                this.Password = hash;
            }
        }


        public bool CheckPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var buffer = Encoding.UTF8.GetBytes(password);

                var hash = sha256.ComputeHash(buffer);

                return this.Password.SequenceEqual(hash);
            }
        }

    }
}

