using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbInterfaces
{
    internal interface IUserUserBookDB
    {
        Task<UserBook> CreateUserBook(UserBook book);

        Task<UserBook[]> GetUserBookByParams(Book book, int price, string condition);

        Task<UserBook> UpdateUserBook(UserBook book, UserBook newUserBook);
        Task<UserBook> DeleteUserBook(UserBook book);
    }
}
