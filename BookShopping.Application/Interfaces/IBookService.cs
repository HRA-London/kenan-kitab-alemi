using System;
using BookShopping.Domain.Entities;

namespace BookShopping.Application.Interfaces
{
    public interface IBookService
    {
        Book GetBookById(int id);
        List<Book> GetBooks();
    }
}

