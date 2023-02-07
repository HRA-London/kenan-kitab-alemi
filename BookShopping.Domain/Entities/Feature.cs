using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.Domain.Entities
{
    public class Feature:Entity<byte>
    {
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string Icon { get; set; }
        public bool IsActive { get; set; }
    }
}
