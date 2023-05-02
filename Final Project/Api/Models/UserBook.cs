using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{

    public class UserBook
    {
        public UserBook() { }
        public UserBook(DataLayer.Models.UserBook DBUserbook)
        {
            UserUpdateModel APIUser = new UserUpdateModel() { UserName = DBUserbook.user.id, City = DBUserbook.user.City };
            Api.Models.Book APIBook = new Api.Models.Book(DBUserbook.book);
            user = APIUser;
            price = DBUserbook.price;
            book = APIBook;
            condition = DBUserbook.condition;
           
        }
        public UserUpdateModel user { get; set; }
        public int price { get; set; }
        public Book book { get; set; }
        public string condition { get; set; }
        
    }
    public class UserBookSearchParams
    {
        public Book book { get; set; }
        public int price { get; set; }
        public string condition { get; set; }
        
    }
    public class UserBookCreateModel
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
    }
}
