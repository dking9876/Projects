using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Message
    {
        public string source { get; set; }
        public string destination { get; set; }
        public string body { get; set; }
        public DateTime time { get; set; }

    }
}
