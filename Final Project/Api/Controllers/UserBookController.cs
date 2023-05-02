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
        public static async Task<IActionResult> SearchBook([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "userbook/searchbook")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new User");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var bookParams = JsonConvert.DeserializeObject<UserBookSearchParams>(requestBody);
            DataLayer.Models.Book DBbook = bookParams.book.GetBookDB();

            UserBookDB BookDb = new UserBookDB();
            try
            {
                var UserBookArrayDB = await BookDb.GetUserBookByParams(DBbook, bookParams.price, bookParams.condition);
                var UserBookArrayAPI = new Models.UserBook[UserBookArrayDB.Length];
                
                if (UserBookArrayDB.Length == 0)
                {
                    return new StatusCodeResult(404);
                    //return null;
                }
                for (int i = 0; i < UserBookArrayDB.Length; i++ )
                {
                    UserBookArrayAPI[i] = new Api.Models.UserBook(UserBookArrayDB[i]);
                }

                return new OkObjectResult(UserBookArrayAPI);
                
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(404);
                
            }
        }
    }
}
