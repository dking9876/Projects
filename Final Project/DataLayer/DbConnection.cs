using Microsoft.Azure.Cosmos;

namespace DataLayer
{

    public class DbConnection
    {
        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        public Container container;

        // The name of the database and container we will create
        private string databaseId = "BooksStore";
        private string containerId = "BooksStore";
   
        public void Connect(string EndpointUri, string PrimaryKey)
        {
            // Create a new instance of the Cosmos Client
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
            this.database = this.cosmosClient.GetDatabase(databaseId);
            this.container = this.database.GetContainer(containerId);
            
        }
    }
}
