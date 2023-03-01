using DataLayer.DbInterfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class BookTests
    {
        static public async Task CreateBookAsync()
        {
            Book book = new Book() { id = "new1", City = "RamatGan", name = "math b", writer = "yoel geva", subject = "math", classNum = 10 };
            BookDB bookDb = new BookDB();
            await bookDb.CreateBook(book);
        }
        static public async Task TestDbGetBookAsync()
        {
            Book book = new Book() { id = "math b yoel geva", City = "RamatGan", name = "math b", writer = "yoel geva", subject = "math", classNum = 10 };
            BookDB bookDb = new BookDB();
            await bookDb.CreateBook(book);
            Book book1 = await bookDb.GetBook("math b yoel geva");
            Console.WriteLine(book1.name);
            Console.WriteLine(book1.City);
            Console.WriteLine(book1.classNum);
        }

        static public async Task TestDbUpdateBookAsync()
        {
            Book book = new Book() { id = "math b yoel geva", City = "RamatGan", name = "math b", writer = "yoel geva", subject = "math", classNum = 10 };
            BookDB bookDb = new BookDB();
            //await bookDb.CreateBook(book);
            await bookDb.UpdateBookname(book, "math b yoel geva");

        }
        static public async Task TestDBDeleteBookAsync()
        {
            Book book = new Book() { id = "new1", City = "RamatGan", name = "math b", writer = "yoel geva", subject = "math", classNum = 10 };
            BookDB bookDb = new BookDB();
            //await bookDb.CreateBook(book);
            await bookDb.DeleteBook(book);

        }
    }
}
