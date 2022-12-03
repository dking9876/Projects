using DataLayer.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;
using User = DataLayer.Models.User;

namespace DataLayer.DbInterfaces
{
    public class UserDB : IUserDB
    {
        private DbConnection db;
        public UserDB(DbConnection db) 
        {
            this.db = db;
        }
        public async Task<User> CreateUser(User user)
        {
            Console.WriteLine("1");
            try
            {
                // Read the item to see if it exists
                ItemResponse<User> UserResponse = await db.container.ReadItemAsync<User>( user.UserName, new PartitionKey("hh"));
                Console.WriteLine("Item in database with id: {0} already exists\n", UserResponse.Resource.UserName);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                
                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                ItemResponse<User> UserResponse = await this.db.container.CreateItemAsync<User>(user, new PartitionKey(""));
                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", UserResponse.Resource.UserName, UserResponse.RequestCharge);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceptin");
            }

            return null;
        }
        public Task<User> GetUser(string username)
        {
            throw new NotImplementedException();
        }

        Task<List<User>> IUserDB.GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
