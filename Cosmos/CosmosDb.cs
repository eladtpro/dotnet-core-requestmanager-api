using Microsoft.Azure.Cosmos;
using RequestManager.Model;

namespace RequestManager.Cosmos
{
    public class CosmosDb
    {
        private readonly Database database;
        public CosmosDb(Database database)
        {
            this.database = database;
        }

        public Container GetContainer<TEntity>()
            where TEntity : Entity
        {
            string conntainerName = nameof(TEntity);
            // TODO: use elegant scenario solution 
            return database.CreateContainerIfNotExistsAsync(conntainerName, "/id").Result.Container;
        }

    }
}
