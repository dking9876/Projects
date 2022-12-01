
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

namespace Api.Controllers
{
    public static class UserApi
    {
        public static readonly List<User> Items = new List<User>();

        [FunctionName("CreateUser")]
        public static async Task<IActionResult> CreateUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new User");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<UserCreateModel>(requestBody);

            var user = new User() { UserName = input.UserName };
            return new OkObjectResult(user);
        }

        [FunctionName("GetAllMissions")]
        public static IActionResult GetAllMissions([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Getting User list items");
            return new OkObjectResult(Items);
        }

        [FunctionName("GetMissionById")]
        public static IActionResult GetMissionById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{username}")] HttpRequest req, ILogger log, string userName)
        {
            var mission = Items.FirstOrDefault(t => t.UserName == userName);
            if (mission == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(mission);
        }

        [FunctionName("UpdateUser")]
        public static async Task<IActionResult> UpdateMission([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "user/{username}")] HttpRequest req, ILogger log, string userName)
        {
            var mission = Items.FirstOrDefault(t => t.UserName == userName);
            if (mission == null)
            {
                return new NotFoundResult();
            }
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<UserUpdateModel>(requestBody);

            return new OkObjectResult(mission);
        }

        [FunctionName("DeleteMission")]
        public static IActionResult DeleteMission([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "user/{username}")] HttpRequest req, ILogger log, string userName)
        {
            var user = Items.FirstOrDefault(t => t.UserName == userName);
            if (user == null)
            {
                return new NotFoundResult();
            }
            Items.Remove(user);
            return new OkResult();
        }
    }
}
