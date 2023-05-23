using System;
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
                // Create an item in the container representing the message. 
                ItemResponse<Message> MessageResponse = await this.db.container.CreateItemAsync<Message>(message, new PartitionKey(message.City));
        
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", MessageResponse.Resource.id, MessageResponse.RequestCharge);
            }
            catch (Exception ex)
            {
                throw new Exception("Eror");
            }

            return message;
        }

        public async Task<Message> DeleteMessage(Message message)
        {
            ItemResponse<Message> messageResponse = await db.container.DeleteItemAsync<Message>(message.id, new PartitionKey(message.City));
            Console.WriteLine("Deleted Message [{0},{1}]\n", message.id, message.City);
            return null;
        }

        public async Task<Message[]> GetAllMessageSentByUser(string username)
        {
            try
            {
                var sqlQueryText = "SELECT * FROM c WHERE c.source = " + "'" + username + "'";

                Console.WriteLine("Running query: {0}\n", sqlQueryText);

                QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                using FeedIterator<Message> queryResultSetIterator = db.container.GetItemQueryIterator<Message>(queryDefinition);
                FeedResponse<Message> currentResultSet = await queryResultSetIterator.ReadNextAsync();

                return currentResultSet.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Eror");
            }


        }

        public async Task<Message[]> GetAllMessageToUser(string username)
        {
            try
            {
                var sqlQueryText = "SELECT * FROM c WHERE c.destination= " + "'" + username + "'";

                Console.WriteLine("Running query: {0}\n", sqlQueryText);

                QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
                using FeedIterator<Message> queryResultSetIterator = db.container.GetItemQueryIterator<Message>(queryDefinition);
                FeedResponse<Message> currentResultSet = await queryResultSetIterator.ReadNextAsync();

                return currentResultSet.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("Eror");
            }
            
        }
    }
}
