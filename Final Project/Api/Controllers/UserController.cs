
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
    
    public static class UserController
    {
        static HttpListenerServer server = new HttpListenerServer();
        //public static readonly List<DataLayer.Models.User> Items = new List<DataLayer.Models.User>();

        [FunctionName("CreateUser")]
        public static async Task<IActionResult> CreateUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new User");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            
            var APIuser = JsonConvert.DeserializeObject<UserCreateModel>(requestBody);
            var DBuser = new DataLayer.Models.User() { id = APIuser.UserName, UserName = APIuser.UserName, Password = APIuser.Password, City = APIuser.City };
            
            UserDB userDb = new UserDB();
            try
            {
                var CreatedDBuser = await userDb.CreateUser(DBuser);
                var UserAPIMOdel = new UserUpdateModel() { UserName = CreatedDBuser.UserName, City = CreatedDBuser.City };
                return new OkObjectResult(UserAPIMOdel);
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

        [FunctionName("Login")]
        public static async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/{username}/login")] HttpRequest req, ILogger log, string userName)
        {
            log.LogInformation("Creating a new User");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var APIuser = JsonConvert.DeserializeObject<UserCreateModel>(requestBody);

            UserDB userDb = new UserDB();
            try
            {
                var Checkuser = await userDb.CheckUser(APIuser.UserName, APIuser.Password, APIuser.City);
                return new OkObjectResult(true);

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(401);
            }
            
        }
        /*
        [FunctionName("GetAllUsers")]
        public static IActionResult GetAllMissions([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user")] HttpRequest req, ILogger log)
        {
            log.LogInformation("Getting User list items");
            return new OkObjectResult(Items);
        }

        [FunctionName("GetUserByName")]
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
        */

        [FunctionName("DeleteUser")]
        public static async Task<IActionResult> DeleteMission([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "user/{username}")] HttpRequest req, ILogger log, string userName)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
           
            var APIuser = JsonConvert.DeserializeObject<UserCreateModel>(requestBody);
            var DBuser = new DataLayer.Models.User() { id = APIuser.UserName, UserName = APIuser.UserName, Password = APIuser.Password, City = APIuser.City };
            UserDB userDb = new UserDB();
            try
            {
                var deleteUserResponse = await userDb.DeleteUser(DBuser);
                return new OkResult();
            }
            catch (Exception ex) 
            {
                return new StatusCodeResult(404);
            }  
        }
    }
}
