using Microsoft.Azure.Cosmos;

namespace DAL
{
    internal class ConnectDatabase
    {
        private static readonly string EndpointUri = "https://localhost:8081";
        private static readonly string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        public CosmosClient? Client;

        public ConnectDatabase()
        {
            Client = new CosmosClient(EndpointUri, PrimaryKey);
        }
    }
}
