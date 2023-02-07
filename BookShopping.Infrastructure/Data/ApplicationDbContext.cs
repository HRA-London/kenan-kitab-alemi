using System;
using BookShopping.Domain.Entities;

namespace BookShopping.Infrastructure.Data
{
    public static class ApplicationDbContext
    {
        public static List<Book> Books { get; set; } = new List<Book>()
        {
            new Book
            {
                Name = "Varli ata, Kasib ata",
                Author = "Terlan Usubov",
                Id = 1
            },
            new Book
            {
                Name = "Kenan esgerlikden qacisi",
                Author = "Vaynkomat",
                Id = 2
            }
        };
    }
}

