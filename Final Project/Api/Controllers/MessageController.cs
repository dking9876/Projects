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
    public static class MessageController
    {
        [FunctionName("CreateMessage")]
        public static async Task<IActionResult> CreateMessage([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "message/create")] HttpRequest req, ILogger log)
        {
            TokenLogic HandleToken = new TokenLogic(req.Headers["Authorization"]);
            if (!HandleToken.IsValid)
            {
                return new StatusCodeResult(401);
            }

            log.LogInformation("CreateMessage");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var response = await MessageLogic.CreateMessage(HandleToken.UserName, requestBody);
            return (response == null) ? new StatusCodeResult(500) : new OkObjectResult(response);
        }
        
        [FunctionName("SentMessages")]
        public static async Task<IActionResult> GetSentMessages([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "message/sent")] HttpRequest req, ILogger log)
        {
            TokenLogic HandleToken = new TokenLogic(req.Headers["Authorization"]);
            if (!HandleToken.IsValid)
            {
                return new StatusCodeResult(401);
            }

            log.LogInformation("GetSentMessages");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var response = await MessageLogic.GetSentMessages(HandleToken.UserName);
            return (response == null) ? new StatusCodeResult(404) : new OkObjectResult(response);
        }
        
        [FunctionName("MyMessages")]
        public static async Task<IActionResult> GetMyMessages([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "message/get")] HttpRequest req, ILogger log)
        {
            TokenLogic HandleToken = new TokenLogic(req.Headers["Authorization"]);
            if (!HandleToken.IsValid)
            {
                return new StatusCodeResult(401);
            }

            log.LogInformation("GetMyMessages");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var response = await MessageLogic.GetMyMessages(HandleToken.UserName);
            return (response == null) ? new StatusCodeResult(404) : new OkObjectResult(response);
        }
    }
}
