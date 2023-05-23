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
        {  
            try
            {
                // Read the item to see if it exists
                ItemResponse<User> UserResponse = await db.container.ReadItemAsync<User>(user.id, new PartitionKey(user.City));

                Console.WriteLine("Item in database with id: {0} already exists\n", UserResponse.Resource.UserName);
                throw new UserExistsException($"User {user.id} in city {user.City} already exsist");
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {

                // Create an item in the container representing the User. 
                ItemResponse<User> UserResponse = await this.db.container.CreateItemAsync<User>(user, new PartitionKey(user.City));
                
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", UserResponse.Resource.UserName, UserResponse.RequestCharge);
            }
            return user;
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
            try
            {
                // Read the item to see if it exists
                ItemResponse<User> UserResponse = await db.container.ReadItemAsync<User>(user.id, new PartitionKey(user.City));
                var partitionKeyValue = user.City;
                var userId = user.id;
                ItemResponse<User> userResponse = await db.container.DeleteItemAsync<User>(userId, new PartitionKey(partitionKeyValue));
                Console.WriteLine("Deleted User [{0},{1}]\n", partitionKeyValue, userId);
                return null;
                
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("User not found");
            }
            
        }
        public async Task<User> CheckUser(string username, string password, string city)
        {
            var sqlQueryText = "SELECT * FROM c WHERE c.UserName = '" + username + "' and c.Password = '" + password + "' and c.City = '" + city + "'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<User> queryResultSetIterator = db.container.GetItemQueryIterator<User>(queryDefinition);
            FeedResponse<User> currentResultSet = await queryResultSetIterator.ReadNextAsync();

            if (currentResultSet.Count == 0)
            {
                throw new Exception("invalid username or password");
            }
            else
            {
                User user = currentResultSet.First();
                return user;
            }

            
        
        }

    }
    public class UserExistsException : Exception
    {
        public UserExistsException(string message) : base(message) { }
    }
}