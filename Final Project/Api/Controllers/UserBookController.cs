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
        public static async Task<Models.UserBook>[] SearchBook([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "book")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new User");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var bookParams = JsonConvert.DeserializeObject<UserBookSearchParams>(requestBody);
            

            UserBookDB BookDb = new UserBookDB();
            try
            {

                var UserBookArray = await UserBookDB.GetUserBookByParams(bookParams.book, bookParams.price, bookParams.condition);
                
                return UserBookArray;
            }
            catch (UserExistsException ex)
            {
                return new ConflictObjectResult($"User {APIuser.UserName} in city {APIuser.City} already exsist");
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
