using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ApiLogic.Logic;
using ApiLogic.Models;

namespace Api.Controllers
{

    public static class UserController
    {
        static HttpListenerServer server = new HttpListenerServer();
        //public static readonly List<DataLayer.Models.User> Items = new List<DataLayer.Models.User>();

        [FunctionName("CreateUser")]
        public static async Task<IActionResult> CreateUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/create")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new User");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var response = await UserLogic.CreateUser(requestBody);
            if(response == null)
            {
                return new StatusCodeResult(409);
            }

            return new OkObjectResult(response);
        }

        [FunctionName("Login")]
        public static async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/login")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new User");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            (bool, string) response =  await UserLogic.Login(requestBody);
            if(response.Item1)
            {
                string str = Token.CreateToken(response.Item2);
                return new OkObjectResult(str);
            }
            else
            {
                return new StatusCodeResult(401);
            }  
        }
    }
}
