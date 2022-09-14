
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

namespace RestFuncApp
{
    public static class MissionApi
    {   
        public static readonly List<Mission> Items = new List<Mission>();

        [FunctionName("CreateMission")]
        public static async Task<IActionResult> CreateMission(
            [HttpTrigger(AuthorizationLevel.Anonymous,
                "post", Route = "mission")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("Creating a new Mission list item");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<MissionCreateModel>(requestBody);

            var mission = new Mission() { MissionDescription = input.MissionDescription };
            Items.Add(mission);
            return new OkObjectResult(mission);
        }

        [FunctionName("GetAllMissions")]
        public static IActionResult GetAllMissions(
            [HttpTrigger(AuthorizationLevel.Anonymous,
                "get", Route = "mission")]
            HttpRequest req, ILogger log)
        {
            log.LogInformation("Getting Mission list items");
            return new OkObjectResult(Items);
        }

        [FunctionName("GetMissionById")]
        public static IActionResult GetMissionById(
            [HttpTrigger(AuthorizationLevel.Anonymous,
                "get", Route = "mission/{id}")]
            HttpRequest req,
            ILogger log, string id)
        {
            var mission = Items.FirstOrDefault(t => t.Id == id);
            if (mission == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(mission);
        }

        [FunctionName("UpdateMission")]
        public static async Task<IActionResult> UpdateMission(
            [HttpTrigger(AuthorizationLevel.Anonymous,
                "put", Route = "mission/{id}")]
            HttpRequest req,
            ILogger log, string id)
        {
            var mission = Items.FirstOrDefault(t => t.Id == id);
            if (mission == null)
            {
                return new NotFoundResult();
            }
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<MissionUpdateModel>(requestBody);

            mission.IsCompleted = updated.IsCompleted;
            if (!string.IsNullOrEmpty(updated.MissionDescription))
            {
                mission.MissionDescription = updated.MissionDescription;
            }
            return new OkObjectResult(mission);
        }

        [FunctionName("DeleteMission")]
        public static IActionResult DeleteMission(
            [HttpTrigger(AuthorizationLevel.Anonymous, 
                "delete", Route = "mission/{id}")]
            HttpRequest req,
            ILogger log, string id)
        {
            var mission = Items.FirstOrDefault(t => t.Id == id);
            if (mission == null)
            {
                return new NotFoundResult();
            }
            Items.Remove(mission);
            return new OkResult();
        }
    }
}
