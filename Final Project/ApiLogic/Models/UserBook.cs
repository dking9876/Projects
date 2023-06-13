using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLogic.Models
{

    public class UserBook
    {
        public UserBook() { }
        public UserBook(DataLayer.Models.UserBook DBUserbook)
        {
            city = DBUserbook.City;
            username = DBUserbook.username;
            price = DBUserbook.price;
            bookname = DBUserbook.bookname;
            condition = DBUserbook.condition;
           
        }
        public string city { get; set; }
        public string username { get; set; }
        public int price { get; set; }
        public string bookname { get; set; }
        public string condition { get; set; }
        public DataLayer.Models.UserBook GetUserBookDB()
        {
            Guid obj = Guid.NewGuid();
            return new DataLayer.Models.UserBook() { id = $"{obj}", City = this.city, username = username, price = price, bookname = bookname, condition = condition };

        }
    }
    public class UserBookSearchParams
    {
        public string city { get; set; }
        public string bookname { get; set; }
        public int price { get; set; }
        public string condition { get; set; }
        
    }
    /*public class UserBookCreateModel
    {
        public UserBookCreateModel() { }
        public UserBookCreateModel(DataLayer.Models.UserBook DBUserbook)
        {
            UserCreateModel APIUser = new UserCreateModel() { UserName = DBUserbook.user.id, Password = DBUserbook.user.Password, City = DBUserbook.user.City };
            Api.Models.Book APIBook = new Api.Models.Book(DBUserbook.book);
            user = APIUser;
            price = DBUserbook.price;
            book = APIBook;
            condition = DBUserbook.condition;

        }
        public UserCreateModel user { get; set; }
        public int price { get; set; }
        public Book book { get; set; }
        public string condition { get; set; }
        public DataLayer.Models.UserBook GetUserBookDB()
        {
            DataLayer.Models.User Dbuser = user.GetUserDB();
            DataLayer.Models.Book Dbbook = book.GetBookDB();
            return new DataLayer.Models.UserBook() { id = Dbuser.id, City = Dbuser.City, user = Dbuser, price = price, book = Dbbook, condition = condition };

        }
    }*/
}
