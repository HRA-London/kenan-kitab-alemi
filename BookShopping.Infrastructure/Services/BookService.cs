using System;
using BookShopping.Application.Interfaces;
using BookShopping.Domain.Entities;
using BookShopping.Infrastructure.Data;

namespace BookShopping.Infrastructure.Services
{
    public class BookService : IBookService
    {

        public Book GetBookById(int id)
        {
            var book = ApplicationDbContext
                                         .Books
                                         .Where(c => c.Id == id)
                                         .FirstOrDefault();

            return book;
        }

        public List<Book> GetBooks()
        {
            var books = new List<Book>()
                                     {
                                         new Book
                                         {
                                             Name = "Varli ata, Kasib ata",
                                             Author = "Terlan Usubov",
                                             Id = 1
                                         }
                                     };
            return books;
        }
    }
}

