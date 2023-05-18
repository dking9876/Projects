using DataLayer.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;
using UserBook = DataLayer.Models.UserBook;

namespace DataLayer.DbInterfaces
{
    public class UserBookDB : IUserUserBookDB
    {
        private DbConnection db;

        public UserBookDB()
        {
            db = new DataLayer.DbConnection();
            db.Connect("UsersBooks");
        }
        public async Task<UserBook> CreateUserBook(UserBook userbook)
        {
            
            try
            {
                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                ItemResponse<UserBook> UserBookResponse = await db.container.CreateItemAsync<UserBook>(userbook, new PartitionKey(userbook.City));
                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", UserBookResponse.Resource.id, UserBookResponse.RequestCharge);
            }
            catch (Exception ex)
            {
                throw new Exception("Eror");
            }

            return userbook;
        }

        public async Task<UserBook> DeleteUserBook(UserBook userbook)
        {
              
            
            try
            {
                var sqlQueryText = "SELECT * FROM c WHERE c.bookname = '" + userbook.bookname + "' and c.price = " + userbook.price + " and c.condition = '" + userbook.condition + "' and c.City = '" + userbook.City + "'";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                using FeedIterator<UserBook> queryResultSetIterator = db.container.GetItemQueryIterator<UserBook>(queryDefinition);
                FeedResponse<UserBook> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                string id = currentResultSet.First().id;
                ItemResponse<UserBook> bookResponse = await db.container.DeleteItemAsync<UserBook>(id, new PartitionKey(userbook.City));
                Console.WriteLine("Deleted UserBook [{0},{1}]\n", userbook.id, userbook.City);
                return null;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("UserBook not found");
            }
            catch (Exception ex)
            {
                throw new Exception("book not found");
            }


        }

        public async Task<UserBook[]> GetUserBookByParams(string bookname, int price, string condition, string city)
        {
            try
            {
                var sqlQueryText = "SELECT * FROM c WHERE c.bookname = '" + bookname + "' and c.price = " + price + " and c.condition = '" + condition + "' and c.City = '" + city + "'";

                Console.WriteLine("Running query: {0}\n", sqlQueryText);

                QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                using FeedIterator<UserBook> queryResultSetIterator = db.container.GetItemQueryIterator<UserBook>(queryDefinition);
                FeedResponse<UserBook> currentResultSet = await queryResultSetIterator.ReadNextAsync();

                return currentResultSet.ToArray();

            }
            catch(Exception ex)
            {
                throw new Exception("book not found");
            }
            
        }
        public async Task<UserBook[]> GetAllUserBooksCreatedByUser(string username)
        {
            try
            {
                var sqlQueryText = "SELECT * FROM c WHERE c.username = " + "'" + username + "'";

                Console.WriteLine("Running query: {0}\n", sqlQueryText);

                QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                using FeedIterator<UserBook> queryResultSetIterator = db.container.GetItemQueryIterator<UserBook>(queryDefinition);
                FeedResponse<UserBook> currentResultSet = await queryResultSetIterator.ReadNextAsync();

                return currentResultSet.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Eror");
            }
        }

            /*public async Task<UserBook> UpdateUserBook(UserBook userbook, UserBook newUserBook)
            {
                ItemResponse<UserBook> userbookResponse = await db.container.ReadItemAsync<UserBook>(userbook.id, new PartitionKey(userbook.City));
                userbookResponse.Resource.price = newUserBook.price;
                userbookResponse.Resource.book = newUserBook.book;
                userbookResponse.Resource.book = newUserBook.book;
                userbookResponse.Resource.condition = newUserBook.condition;

                // replace the item with the updated content
                userbookResponse = await db.container.ReplaceItemAsync<UserBook>(userbookResponse.Resource, userbook.id, new PartitionKey(userbook.City));
                return null;
            }*/
        }
}
