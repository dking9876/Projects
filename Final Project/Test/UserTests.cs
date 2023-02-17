﻿using DataLayer.DbInterfaces;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class UserTests
    {
        
        static public async Task CreateUserAsync()
        {
            User user = new User() { id = "Daniel", City = "RamatGan", UserName = "Daniel", Password = "123" };
            UserDB userDb = new UserDB();
            await userDb.CreateUser(user);
        }
        static public async Task TestDbGetUserAsync()
        {
            User user = new User() { id = "Daniel", City = "RamatGan", UserName = "Daniel", Password = "123" };
            UserDB userDb = new UserDB();
            await userDb.CreateUser(user);
            User user1 = await userDb.GetUser("Daniel");
            Console.WriteLine(user1.UserName);
            Console.WriteLine(user1.City);
        }
    }
}



