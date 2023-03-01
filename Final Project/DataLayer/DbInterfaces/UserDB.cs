using DataLayer.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using System;
using System.Collections.Concurrent;
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
        
        public UserDB()
        {
            db = new DataLayer.DbConnection();
            db.Connect("Users");
        }

        public async Task<User> CreateUser(User user)
<<<<<<< HEAD
        {  
            try
            {
                // Read the item to see if it exists
                ItemResponse<User> UserResponse = await db.container.ReadItemAsync<User>( user.UserName, new PartitionKey(""));
=======
        {
            try
            {
                // Read the item to see if it exists
                ItemResponse<User> UserResponse = await db.container.ReadItemAsync<User>(user.id, new PartitionKey(user.City));
>>>>>>> 8b2a7f08ccfd251fbd2f89af96ba0fe5eb92644a
                Console.WriteLine("Item in database with id: {0} already exists\n", UserResponse.Resource.UserName);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {

                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                ItemResponse<User> UserResponse = await this.db.container.CreateItemAsync<User>(user, new PartitionKey(user.City));
                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", UserResponse.Resource.UserName, UserResponse.RequestCharge);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceptin");
            }

            return null;
        }
        public async Task<User> GetUser(string username)
        {
            
            var sqlQueryText = "SELECT * FROM c WHERE c.UserName = " + "'" + username + "'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<User> queryResultSetIterator = db.container.GetItemQueryIterator<User>(queryDefinition);
            FeedResponse<User> currentResultSet = await queryResultSetIterator.ReadNextAsync();   

            if(currentResultSet.Count != 1)
            {
                throw new Exception("User not found");
            }
            return currentResultSet.First();
        }

        public async Task<User> UpdateUsername(User user, string newUsername)
        {
            ItemResponse<User> userResponse = await db.container.ReadItemAsync<User>(user.id, new PartitionKey(user.City));
            userResponse.Resource.UserName = newUsername;

            // replace the item with the updated content
            userResponse = await db.container.ReplaceItemAsync<User>(userResponse.Resource, user.id, new PartitionKey(user.City));
            return null;
        }
        public async Task<User> DeleteUser(User user)
        {
            var partitionKeyValue = user.City;
            var userId = user.id;
            ItemResponse<User> userResponse = await db.container.DeleteItemAsync<User>(userId, new PartitionKey(partitionKeyValue));
            Console.WriteLine("Deleted Family [{0},{1}]\n", partitionKeyValue, userId);
            return null;
        }




    }
}