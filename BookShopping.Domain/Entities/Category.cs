using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopping.Domain.Entities
{
    public class Category:Entity<int>
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public bool IsActive { get; set; } 
        public int Priority { get; set; }


    }
}
