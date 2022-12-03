using DataLayer.DbInterfaces;
using DataLayer.Models;
using System;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestDbUserAsync().Wait();
        }
        static public void  TestDbConnection()
        {
            var db = new DataLayer.DbConnection();
            db.Connect("https://booksstore.documents.azure.com:443/", "9hRgHS70P6B2vo4KrK34Os75nFNDrp3N8eE8w6YRH0MYppNhjnUlGptXbEMKAC5EPe5ENVryQnucACDbWJyClw==");
        }
        static public async Task TestDbUserAsync()
        {
            User user = new User() { UserName = "Daniel", Password = "123" };
            //User user = new User("Mark", "123");
            var db = new DataLayer.DbConnection();
            db.Connect("https://booksstore.documents.azure.com:443/", "9hRgHS70P6B2vo4KrK34Os75nFNDrp3N8eE8w6YRH0MYppNhjnUlGptXbEMKAC5EPe5ENVryQnucACDbWJyClw==");
            UserDB DBuser = new UserDB(db);
            await DBuser.CreateUser(user);       
        }
    }
}
