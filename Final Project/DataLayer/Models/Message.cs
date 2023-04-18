using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Message : DbItem
    {
        public string source { get; set; }
        public string destination { get; set; }
        public string body { get; set; }
        public DateTime time  { get; set; }
    }
}
