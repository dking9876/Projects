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
    public static class BookController
    {
        [FunctionName("GetAllBooks")]
        static public async Task<IActionResult> GetAllBooks([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "book")] HttpRequest req, ILogger log)
        {
            BookDB bookDb = new BookDB();
            DataLayer.Models.Book[] DBbookary = await bookDb.GetAllBooks();
            Models.Book[] APIBookary = new Models.Book[DBbookary.Length];
            for (int i = 0; i < DBbookary.Length; i++)
            {
                APIBookary[i] = new Api.Models.Book(DBbookary[i]);
            }

            return new OkObjectResult(APIBookary);
        }
    }
}
