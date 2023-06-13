using DataLayer.DbInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DbInterfaces;
using DataLayer.Models;

namespace Test
{
    internal class MessageTests
    {
        static public async Task CreateMessageAsync()
        {
            DateTime currentDateTimeUTC = DateTime.UtcNow;
            Message message = new Message() { id = "3", City = "RamatGan", source = "daniel", destination = "mark", body = "hello3", time = currentDateTimeUTC };
            MessageDB messageDb = new MessageDB();
            await messageDb.CreateMessage(message);
        }
        static public async Task TestDBDeleteMessagAsync()
        {
            DateTime currentDateTimeUTC = DateTime.UtcNow;
            Message message = new Message() { id = "2", City = "RamatGan", source = "daniel", destination = "mark", body = "hello", time = currentDateTimeUTC };
            MessageDB messageDb = new MessageDB();
            //await userDb.CreateMessage(user);
            await messageDb.DeleteMessage(message);

        }
        static public async Task TestDbGetAllMessageSentByUserAsync()
        {
            User user = new User() { id = "Daniel", City = "RamatGan", UserName = "daniel", Password = "123" };
            MessageDB messageDb = new MessageDB();
            //await userDb.CreateMessage(user);
            //Message[] msgArray = await messageDb.GetAllMessageSentByUser(user);
            //Console.WriteLine(msgArray[0].body);
            //Console.WriteLine(msgArray[1].body);
            //Console.WriteLine(msgArray[2].body);
        }
        static public async Task TestDbGetAllMessageToUserAsync()
        {
            User user = new User() { id = "Daniel", City = "RamatGan", UserName = "mark", Password = "123" };
            MessageDB messageDb = new MessageDB();
            //await userDb.CreateMessage(user);
            //Message[] msgArray = await messageDb.GetAllMessageToUser(user);
            //Console.WriteLine(msgArray[0].body);
            //Console.WriteLine(msgArray[1].body);
            //Console.WriteLine(msgArray[2].body);
        }
    }
}
