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
            Book book1 = new Book() { id = "new1", City = "RamatGan", name = "math a", writer = "yoel geva", subject = "math", classNum = 8 };
            Book book2 = new Book() { id = "new2", City = "RamatGan", name = "math b", writer = "yoel geva", subject = "math", classNum = 9 };
            Book book3 = new Book() { id = "new3", City = "RamatGan", name = "math c", writer = "yoel geva", subject = "math", classNum = 10 };
            BookDB bookDb = new BookDB();
            await bookDb.CreateBook(book1);
            await bookDb.CreateBook(book2);
            await bookDb.CreateBook(book3);
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
        static public async Task TestDBGetAllBooks()
        {
            
            BookDB bookDb = new BookDB();
            Book[] bookary = await bookDb.GetAllBooks();
            for(int i = 0; i < bookary.Length; i++)
            {
                Console.WriteLine(bookary[i].id);
            }
            

        }
    }
}
