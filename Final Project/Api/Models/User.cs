﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Models
{
    public class UserCreateModel
    {
        public string UserName { get; set; }
        public int Password { get; set; }
    }

    public class UserUpdateModel
    {
    }
}