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
            try
            {
                // Read the item to see if it exists
                ItemResponse<User> UserResponse = await db.container.ReadItemAsync<User>(user.id, new PartitionKey(user.City));
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
            /*
            var sqlQueryText = "SELECT * FROM c WHERE c.UserName = username";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<User> queryResultSetIterator = db.container.GetItemQueryIterator<User>(queryDefinition);

            List<User> Users = new List<User>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<User> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (User user in currentResultSet)
                {
                    Users.Add(user);
                    Console.WriteLine("\tRead {0}\n", user);
                }
            }
            */
            return null;
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUsers()
        {
            /*
            var sqlQueryText = "SELECT * FROM c";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<User> queryResultSetIterator = db.container.GetItemQueryIterator<User>(queryDefinition);

            List<User> Users = new List<User>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<User> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (User user in currentResultSet)
                {
                    Users.Add(user);
                    Console.WriteLine("\tRead {0}\n", user);
                }
            }
            */
            return null;

        }

    }
}