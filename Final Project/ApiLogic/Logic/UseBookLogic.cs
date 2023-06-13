using ApiLogic.Models;
using DataLayer.DbInterfaces;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLogic.Logic
{
    public class UserBookLogic
    {
        public static async Task<UserBook[]> SearchBook( string body)
        {
            var bookParams = JsonConvert.DeserializeObject<UserBookSearchParams>(body);

            UserBookDB BookDb = new UserBookDB();
            try
            {
                var UserBookArrayDB = await BookDb.GetUserBookByParams(bookParams.bookname, bookParams.price, bookParams.condition, bookParams.city);
                var UserBookArrayAPI = new Models.UserBook[UserBookArrayDB.Length];

                if (UserBookArrayDB.Length == 0)
                {
                    return null;
                }
                for (int i = 0; i < UserBookArrayDB.Length; i++)
                {
                    UserBookArrayAPI[i] = new ApiLogic.Models.UserBook(UserBookArrayDB[i]);
                }

                return UserBookArrayAPI;

            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public static async Task<UserBook> CreateUserBook(string username,string body)
        {
            var APIuserbook = JsonConvert.DeserializeObject<UserBook>(body);
            APIuserbook.username = username;

            var DBuserbook = APIuserbook.GetUserBookDB();

            UserBookDB userBookDb = new UserBookDB();
            try
            {
                var CreatedDBuserBook = await userBookDb.CreateUserBook(DBuserbook);
                var UserBookAPIMOdel = new Models.UserBook(CreatedDBuserBook);
                return UserBookAPIMOdel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<UserBook> DeleteUserBook(string username, string body)
        {

            var APIuserbook = JsonConvert.DeserializeObject<Models.UserBook>(body);
            APIuserbook.username = username;
            var DBuserbook = APIuserbook.GetUserBookDB();

            UserBookDB userBookDb = new UserBookDB();
            try
            {
                var deleteUserResponse = await userBookDb.DeleteUserBook(DBuserbook);
                return APIuserbook;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static async Task<UserBook[]> GetMyBooks(string username)
        {

            UserBookDB BookDb = new UserBookDB();
            try
            {
                var UserBookArrayDB = await BookDb.GetAllUserBooksCreatedByUser(username);
                var UserBookArrayAPI = new UserBook[UserBookArrayDB.Length];

                if (UserBookArrayDB.Length == 0)
                {
                    return null;
                    //return null;
                }
                for (int i = 0; i < UserBookArrayDB.Length; i++)
                {
                    UserBookArrayAPI[i] = new ApiLogic.Models.UserBook(UserBookArrayDB[i]);
                }

                return  UserBookArrayAPI;

            }
            catch (Exception ex)
            {
                return null;

            }

        }
    }
 }



