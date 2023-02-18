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
using Book = DataLayer.Models.Book;

namespace DataLayer.DbInterfaces
{
    public class BookDB : IBookDB
    {
        private DbConnection db;

        public BookDB()
        {
            db = new DataLayer.DbConnection();
            db.Connect("Books");
        }

        public async Task<Book> CreateBook(Book book)
        {
            try
            {
                // Read the item to see if it exists
                ItemResponse<Book> BookResponse = await db.container.ReadItemAsync<Book>(book.id, new PartitionKey(book.City));
                Console.WriteLine("Item in database with id: {0} already exists\n", BookResponse.Resource.id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {

                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                ItemResponse<Book> BookResponse = await db.container.CreateItemAsync<Book>(book, new PartitionKey(book.City));
                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", BookResponse.Resource.id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceptin");
            }

            return null;
        }
        public async Task<Book> GetBook(string bookId)
        {

            var sqlQueryText = "SELECT * FROM c WHERE c.id = " + "'" + bookId + "'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<Book> queryResultSetIterator = db.container.GetItemQueryIterator<Book>(queryDefinition);
            FeedResponse<Book> currentResultSet = await queryResultSetIterator.ReadNextAsync();

            if (currentResultSet.Count != 1)
            {
                throw new Exception("User not found");
            }
            return currentResultSet.First();
        }

        public async Task<Book> UpdateBookname(Book book, string newbookName)
        {
            ItemResponse<Book> bookResponse = await db.container.ReadItemAsync<Book>(book.id, new PartitionKey(book.City));
            bookResponse.Resource.name = newbookName;

            // replace the item with the updated content
            bookResponse = await db.container.ReplaceItemAsync<Book>(bookResponse.Resource, book.id, new PartitionKey(book.City));
            return null;
        }
        public async Task<Book> DeleteBook(Book book)
        {
            var partitionKeyValue = book.City;
            var bookid = book.id;
            ItemResponse<Book> bookResponse = await db.container.DeleteItemAsync<Book>(bookid, new PartitionKey(partitionKeyValue));
            Console.WriteLine("Deleted Family [{0},{1}]\n", partitionKeyValue, bookid);
            return null;
        }


    }
}