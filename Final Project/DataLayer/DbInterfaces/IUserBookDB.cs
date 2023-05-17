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

        Task<UserBook[]> GetUserBookByParams(string book, int price, string condition, string city);

        //Task<UserBook> UpdateUserBook(UserBook book, UserBook newUserBook);
        Task<UserBook> DeleteUserBook(UserBook book);
        Task<UserBook[]> GetAllUserBooksCreatedByUser(string username);
    }
}
