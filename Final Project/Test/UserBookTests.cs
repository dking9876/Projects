using DataLayer.DbInterfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class UserBookTests
    {
        static public async Task CreateUserBookAsync()
        {
            Book book = new Book() { id = "new1", City = "RamatGan", name = "math b", writer = "yoel geva", subject = "math", classNum = 10 };
            Book book2 = new Book() { id = "new2", City = "RamatGan", name = "math c", writer = "yoel geva", subject = "math", classNum = 10 };
            User user = new User() { id = "Daniel1", City = "RamatGan", UserName = "Daniel", Password = "123" };
            UserBook userBook1 = new UserBook() { id = "book1", City = "RamatGan", user = user, price = 30, book = book, condition = "good" };
            UserBook userBook2 = new UserBook() { id = "book2", City = "RamatGan", user = user, price = 40, book = book, condition = "good" };
            UserBook userBook3 = new UserBook() { id = "book3", City = "RamatGan", user = user, price = 50, book = book2, condition = "good" };
            UserBook userBook4 = new UserBook() { id = "book4", City = "RamatGan", user = user, price = 40, book = book, condition = "great" };
            UserBook userBook5 = new UserBook() { id = "book5", City = "RamatGan", user = user, price = 40, book = book2, condition = "good" };
            UserBookDB userBookDb = new UserBookDB();
            await userBookDb.CreateUserBook(userBook1);
            await userBookDb.CreateUserBook(userBook2);
            await userBookDb.CreateUserBook(userBook3);
            await userBookDb.CreateUserBook(userBook4);
            await userBookDb.CreateUserBook(userBook5);
            UserBook userBook6 = new UserBook() { id = "book6", City = "Tel-Aviv", user = user, price = 40, book = book, condition = "good" };
            await userBookDb.CreateUserBook(userBook6);
        }

        static public async Task TestDBDeleteUserBookAsync()
        {
            Book book2 = new Book() { id = "new2", City = "RamatGan", name = "math c", writer = "yoel geva", subject = "math", classNum = 10 };
            Book book = new Book() { id = "new1", City = "RamatGan", name = "math b", writer = "yoel geva", subject = "math", classNum = 10 };
            User user = new User() { id = "Daniel1", City = "RamatGan", UserName = "Daniel", Password = "123" };
            UserBook userBook = new UserBook() { id = "book1", City = "RamatGan", user = user, price = 40, book = book, condition = "good" };
            UserBook userBook1 = new UserBook() { id = "book1", City = "RamatGan", user = user, price = 30, book = book, condition = "good" };
            UserBook userBook2 = new UserBook() { id = "book2", City = "RamatGan", user = user, price = 40, book = book, condition = "good" };
            UserBook userBook3 = new UserBook() { id = "book3", City = "RamatGan", user = user, price = 50, book = book2, condition = "good" };
            UserBook userBook4 = new UserBook() { id = "book4", City = "RamatGan", user = user, price = 40, book = book, condition = "good" };
            UserBook userBook5 = new UserBook() { id = "book5", City = "RamatGan", user = user, price = 40, book = book2, condition = "good" };
            UserBookDB userBookDb = new UserBookDB();
            UserBook userBook6 = new UserBook() { id = "book6", City = "Tel-Aviv", user = user, price = 40, book = book, condition = "good" };
            await userBookDb.DeleteUserBook(userBook1);
            await userBookDb.DeleteUserBook(userBook2);
            await userBookDb.DeleteUserBook(userBook3);
            await userBookDb.DeleteUserBook(userBook4);
            await userBookDb.DeleteUserBook(userBook5);
            await userBookDb.DeleteUserBook(userBook6);
        }
        static public async Task TestDbGetUserBookByParamsAsync()
            {
            Book book = new Book() { id = "new1", City = "RamatGan", name = "math b", writer = "yoel geva", subject = "math", classNum = 10 };
            UserBookDB userBookDb = new UserBookDB();
            UserBook[] UserBookarr = await userBookDb.GetUserBookByParams(book, 40, "good");
            for (int i = 0; i < UserBookarr.Length; i++ )
            {
                Console.WriteLine(UserBookarr[i].id);
            }


        }
        static public async Task TestDbUpdateUserBookAsync()
        {
            Book book2 = new Book() { id = "new2", City = "RamatGan", name = "math b", writer = "yoel geva", subject = "math", classNum = 10 };
            Book book = new Book() { id = "new1", City = "RamatGan", name = "math b", writer = "yoel geva", subject = "math", classNum = 10 };
            User user = new User() { id = "Daniel1", City = "RamatGan", UserName = "Daniel", Password = "123" };
            UserBook UserBook1 = new UserBook() { id = "book1", City = "RamatGan", user = user, price = 40, book = book, condition = "good" };
            UserBook userBook2 = new UserBook() { id = "book1", City = "RamatGan", user = user, price = 45, book = book2, condition = "not good" };
            UserBookDB userBookDb = new UserBookDB();
            await userBookDb.UpdateUserBook(UserBook1, userBook2);

        }
            
      }
}
