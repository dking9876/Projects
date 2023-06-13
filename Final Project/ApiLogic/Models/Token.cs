using DataLayer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLogic.Models
{
    public class Token
    {
        public string UserName { get; set; }
        public DateTime ExperationTime { get; set; }
        public Token()
        {
        }

        public bool IsValidToken()
        {
            return this.ExperationTime > DateTime.Now;  
        }
    }
}
