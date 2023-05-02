using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbInterfaces
{
    internal interface IBookDB
    {
        Task<Book> CreateBook(Book book);

        Task<Book> GetBook(string bookId);

        Task<Book> UpdateBookname(Book book, string newBookName);
        Task<Book> DeleteBook(Book book);

        Task<Book[]> GetAllBooks();

    }
}
