using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Models
{
    public class UserCreateModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string City { get; set; }


    }

    public class UserUpdateModel
    {
    }
}
