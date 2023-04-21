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
                // Read the item to see if it exists
                ItemResponse<UserBook> UserBookResponse = await db.container.ReadItemAsync<UserBook>(userbook.id, new PartitionKey(userbook.City));
                Console.WriteLine("Item in database with id: {0} already exists\n", UserBookResponse.Resource.id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {

                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                ItemResponse<UserBook> UserBookResponse = await db.container.CreateItemAsync<UserBook>(userbook, new PartitionKey(userbook.City));
                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", UserBookResponse.Resource.id, UserBookResponse.RequestCharge);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceptin");
            }

            return null;
        }

        public async Task<UserBook> DeleteUserBook(UserBook userbook)
        {
            ItemResponse<UserBook> bookResponse = await db.container.DeleteItemAsync<UserBook>(userbook.id, new PartitionKey(userbook.City));
            Console.WriteLine("Deleted UserBook [{0},{1}]\n", userbook.id, userbook.City);
            return null;
        }

        public async Task<UserBook[]> GetUserBookByParams(Book book, int price, string condition)
        {
            var sqlQueryText = "SELECT * FROM c WHERE c.book.name = '" + book.name + "' and c.price = " + price + " and c.condition = '" + condition + "'";
            //var sqlQueryText = "SELECT * FROM c WHERE c.book.name = '" + book.name + "' And c.price = '" + price + "' And c.condition = '" + condition + "'";
            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<UserBook> queryResultSetIterator = db.container.GetItemQueryIterator<UserBook>(queryDefinition);
            FeedResponse<UserBook> currentResultSet = await queryResultSetIterator.ReadNextAsync();

            return currentResultSet.ToArray();
        }

        public async Task<UserBook> UpdateUserBook(UserBook userbook, UserBook newUserBook)
        {
            ItemResponse<UserBook> userbookResponse = await db.container.ReadItemAsync<UserBook>(userbook.id, new PartitionKey(userbook.City));
            userbookResponse.Resource.price = newUserBook.price;
            userbookResponse.Resource.book = newUserBook.book;
            userbookResponse.Resource.book = newUserBook.book;
            userbookResponse.Resource.condition = newUserBook.condition;

            // replace the item with the updated content
            userbookResponse = await db.container.ReplaceItemAsync<UserBook>(userbookResponse.Resource, userbook.id, new PartitionKey(userbook.City));
            return null;
        }
    }
}
