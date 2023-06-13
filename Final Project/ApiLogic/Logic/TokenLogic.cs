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
            var token = StringUtil.Decrypt(authHeader);
            Token AuthorizationToken = JsonConvert.DeserializeObject<Token>(token);
            this.IsValid = AuthorizationToken.IsValidToken();
            this.UserName = AuthorizationToken.UserName;
        }

        public static string CreateToken(string userName)
        {
            Token token = new Token();
            token.UserName = userName; ;
            token.ExperationTime = DateTime.Now.AddHours(48);
            string str = JsonConvert.SerializeObject(token);
            str = StringUtil.Crypt(str);
            return str;
        }
    }
}
