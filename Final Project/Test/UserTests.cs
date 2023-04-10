using DataLayer.DbInterfaces;
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
            User user = new User() { id = "Daniel1", City = "RamatGan", UserName = "Daniel", Password = "123" };
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

        static public async Task TestDbUpdateUserAsync()
        {
            User user = new User() { id = "Daniel1", City = "RamatGan", UserName = "Daniel", Password = "123" };
            UserDB userDb = new UserDB();
            //await userDb.CreateUser(user);
            await userDb.UpdateUsername(user, "mark");
            
        }
        static public async Task TestDBDeleteUserAsync()
        {
            User user = new User() { id = "Daniel", City = "RamatGan", UserName = "Daniel", Password = "123" };
            UserDB userDb = new UserDB();
            //await userDb.CreateUser(user);
            await userDb.DeleteUser(user);

        }
        static public async Task TestDBCheckUserAsync()
        {
            User user = new User() { id = "Daniel", City = "RamatGan", UserName = "Daniel", Password = "123" };
            UserDB userDb = new UserDB();
            //await userDb.CreateUser(user);
            await userDb.CheckUser("mark", "123");

        }
    }
}



