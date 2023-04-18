﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;
using Message = DataLayer.Models.Message;
using Microsoft.Azure.Cosmos;
using System.Net;
using DataLayer.Models;
using User = DataLayer.Models.User;

namespace DataLayer.DbInterfaces
{
    public class MessageDB : IMessageDB
    {
        private DbConnection db;

        public MessageDB()
        {
            db = new DataLayer.DbConnection();
            db.Connect("Messages");
        }
        public async Task<Message> CreateMessage(Message message)
        {
            try
            {
                // Read the item to see if it exists
                ItemResponse<Message> MessageResponse = await db.container.ReadItemAsync<Message>(message.id, new PartitionKey(message.City));

                Console.WriteLine("Item in database with id: {0} already exists\n", MessageResponse.Resource.id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {

                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                ItemResponse<Message> MessageResponse = await this.db.container.CreateItemAsync<Message>(message, new PartitionKey(message.City));
                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", MessageResponse.Resource.id, MessageResponse.RequestCharge);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceptin");
            }

            return null;
        }

        public async Task<Message> DeleteMessage(Message message)
        {
            ItemResponse<Message> messageResponse = await db.container.DeleteItemAsync<Message>(message.id, new PartitionKey(message.City));
            Console.WriteLine("Deleted Message [{0},{1}]\n", message.id, message.City);
            return null;
        }

        public async Task<Message[]> GetAllMessageSentByUser(User user)
        {
           
            var sqlQueryText = "SELECT * FROM c WHERE c.source = " + "'" + user.UserName + "'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<Message> queryResultSetIterator = db.container.GetItemQueryIterator<Message>(queryDefinition);
            FeedResponse<Message> currentResultSet = await queryResultSetIterator.ReadNextAsync();
           
            return currentResultSet.ToArray();
        }

        public async Task<Message[]> GetAllMessageToUser(User user)
        {
            var sqlQueryText = "SELECT * FROM c WHERE c.destanation = " + "'" + user.UserName + "'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            using FeedIterator<Message> queryResultSetIterator = db.container.GetItemQueryIterator<Message>(queryDefinition);
            FeedResponse<Message> currentResultSet = await queryResultSetIterator.ReadNextAsync();

            return currentResultSet.ToArray();
        }
    }
}
