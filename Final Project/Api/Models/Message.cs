using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Message
    {
        public static int count { get; protected set; }

        static Message()
        {
            count = 5;
        }
        
        public Message() { count = count + 1; }
        public Message(DataLayer.Models.Message DBMessage)
        {
            count = count++;
            city = DBMessage.City;
            source = DBMessage.source;
            destination = DBMessage.destination;
            body = DBMessage.body;
            time = DBMessage.time;
        }
        
        public string city { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public string body { get; set; }
        public DateTime time { get; set; }


        public DataLayer.Models.Message GetMessageDB()
        {
            count = count++;
            return new DataLayer.Models.Message() { id = $"{count}", City = this.city, source = source, destination = destination, body = body, time = DateTime.Now };

        }
    }
}
