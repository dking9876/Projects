using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ApiLogic.Logic;

namespace Api.Controllers
{
    public static class UserBookController
    {
        [FunctionName("SearchBook")]
        public static async Task<IActionResult> SearchBook([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "userbook/search")] HttpRequest req, ILogger log)
        {
            TokenLogic HandleToken = new TokenLogic(req.Headers["Authorization"]);
            if(!HandleToken.IsValid)
            {
                  return new StatusCodeResult(401);
            }    

            log.LogInformation("Searching UserBook");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var response = await UserBookLogic.SearchBook(requestBody);

            return (response == null) ? new StatusCodeResult(404) : new OkObjectResult(response);

        }
        [FunctionName("CreateUserBook")]
        public static async Task<IActionResult> CreateUserBook([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "userbook/create")] HttpRequest req, ILogger log)
        {
            TokenLogic HandleToken = new TokenLogic(req.Headers["Authorization"]);
            if (!HandleToken.IsValid)
            {
                return new StatusCodeResult(401);
            }

            log.LogInformation("Creating a new UserBook");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var response = await UserBookLogic.CreateUserBook(HandleToken.UserName, requestBody);
            return (response == null) ? new StatusCodeResult(500) : new OkObjectResult(response);
        }



        [FunctionName("DeleteUserBook")]
        public static async Task<IActionResult> DeleteUserBook([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "userbook/delete")] HttpRequest req, ILogger log)
        {
            TokenLogic HandleToken = new TokenLogic(req.Headers["Authorization"]);
            if (!HandleToken.IsValid)
            {
                return new StatusCodeResult(401);
            }

            log.LogInformation("DeleteUserBook");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var response = await UserBookLogic.DeleteUserBook(HandleToken.UserName, requestBody);
            return (response == null) ? new StatusCodeResult(404) : new OkObjectResult(response);

        }
        [FunctionName("GetMyBooks")]
        public static async Task<IActionResult> GetMyBooks([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "userbook/get")] HttpRequest req, ILogger log)
        {
            TokenLogic HandleToken = new TokenLogic(req.Headers["Authorization"]);
            if (!HandleToken.IsValid)
            {
                return new StatusCodeResult(401);
            }

            log.LogInformation("GetMyBooks");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var response = await UserBookLogic.GetMyBooks(HandleToken.UserName);
            return (response == null) ? new StatusCodeResult(404) : new OkObjectResult(response);
        }


    }
}
