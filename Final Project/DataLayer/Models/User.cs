using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class User : DbItem
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}
