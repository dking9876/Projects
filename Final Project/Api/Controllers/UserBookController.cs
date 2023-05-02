using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DataLayer.Models;
using Api.Models;
using DataLayer;
using DataLayer.DbInterfaces;
using System;
using Microsoft.Azure.Cosmos;

namespace Api.Controllers
{
    public static class UserBookController
    {
        [FunctionName("SearchBook")]
        public static async Task<Models.UserBook[]> SearchBook([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "book")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new User");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var bookParams = JsonConvert.DeserializeObject<UserBookSearchParams>(requestBody);
            DataLayer.Models.Book DBbook = bookParams.book.GetBookDB();

            UserBookDB BookDb = new UserBookDB();
            try
            {
                var UserBookArrayAPI = new Models.UserBook[] { };
                var UserBookArrayDB = await BookDb.GetUserBookByParams(DBbook, bookParams.price, bookParams.condition);
                for (int i = 0; i < UserBookArrayDB.Length; i++ )
                {
                    UserBookArrayAPI[i] = new Api.Models.UserBook(UserBookArrayDB[i]);
                }
                
                if (UserBookArrayDB == null)
                {
                    return new StatusCodeResult(404);
                }
                
                return UserBookArrayAPI;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(404);
            }
        }
    }
}
