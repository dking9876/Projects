using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Api.Models
{
    public class Message
    {
        
        
        public Message() {  }
        public Message(DataLayer.Models.Message DBMessage)
        {
            
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
            Guid obj = Guid.NewGuid();
            return new DataLayer.Models.Message() { id = $"{obj}", City = this.city, source = source, destination = destination, body = body, time = DateTime.Now };

        }
    }
}
