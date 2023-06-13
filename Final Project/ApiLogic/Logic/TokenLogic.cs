using ApiLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiLogic.Logic
{
    public class TokenLogic
    {

        public bool IsValid { get; set; }
        public string UserName { get; set; }
        public TokenLogic(string authHeader) 
        {
            Token AuthorizationToken = JsonConvert.DeserializeObject<Token>(authHeader);
            this.IsValid = AuthorizationToken.IsValidToken();
            this.UserName = AuthorizationToken.UserName;
        }
    }
}
