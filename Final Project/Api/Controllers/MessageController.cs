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
    public static class MessageController
    {
        [FunctionName("CreateMessage")]
        public static async Task<IActionResult> CreateMessage([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "message")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new message");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var APIMessage = JsonConvert.DeserializeObject<Models.Message>(requestBody);

            var DBMessage = APIMessage.GetMessageDB();

            MessageDB message = new MessageDB();
            try
            {
                var CreatedDBmessage = await message.CreateMessage(DBMessage);
                var MessageAPIMOdel = new Models.Message(CreatedDBmessage);
                return new OkObjectResult(MessageAPIMOdel);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        
        [FunctionName("SentMessages")]
        public static async Task<IActionResult> GetSentMessages([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "message/{username}/sentmessages")] HttpRequest req, ILogger log, string userName)
        {
           
            MessageDB message = new MessageDB();
            try
            {
                var SentMessagesArrayDb = await message.GetAllMessageSentByUser(userName);
                var MessagesArrayAPI = new Models.Message[SentMessagesArrayDb.Length];

                if (SentMessagesArrayDb.Length == 0)
                {
                    return new StatusCodeResult(404);
                    //return null;
                }
                for (int i = 0; i < SentMessagesArrayDb.Length; i++)
                {
                    MessagesArrayAPI[i] = new Api.Models.Message(SentMessagesArrayDb[i]);
                }

                return new OkObjectResult(MessagesArrayAPI);

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(404);

            }

        }
        
        [FunctionName("MyMessages")]
        public static async Task<IActionResult> GetMyMessages([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "message/{username}/getmessages")] HttpRequest req, ILogger log, string userName)
        {
            MessageDB message = new MessageDB();
            try
            {
                var SentMessagesArrayDb = await message.GetAllMessageToUser(userName);
                var MessagesArrayAPI = new Models.Message[SentMessagesArrayDb.Length];

                if (SentMessagesArrayDb.Length == 0)
                {
                    return new StatusCodeResult(404);
                    //return null;
                }
                for (int i = 0; i < SentMessagesArrayDb.Length; i++)
                {
                    MessagesArrayAPI[i] = new Api.Models.Message(SentMessagesArrayDb[i]);
                }

                return new OkObjectResult(MessagesArrayAPI);

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(404);

            }
        }
        


    }
}
