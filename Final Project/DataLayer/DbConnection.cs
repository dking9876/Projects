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
        private string endpointUri = "https://booksstore.documents.azure.com:443/";
        private string primaryKey = "9hRgHS70P6B2vo4KrK34Os75nFNDrp3N8eE8w6YRH0MYppNhjnUlGptXbEMKAC5EPe5ENVryQnucACDbWJyClw==";
        public void Connect(string containerId)
        {
            // Create a new instance of the Cosmos Client
            this.cosmosClient = new CosmosClient(endpointUri, primaryKey);
            this.database = this.cosmosClient.GetDatabase(databaseId);
            this.container = this.database.GetContainer(containerId);
            
        }
    }
}
