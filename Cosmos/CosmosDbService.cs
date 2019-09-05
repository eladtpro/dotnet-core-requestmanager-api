using Microsoft.Azure.Cosmos;
using RequestManager.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RequestManager.Cosmos
{
    public abstract class CosmosDbService<TEntity>
        where TEntity : Entity
    {
        private readonly Container container;

        public CosmosDbService(CosmosDb cosmosDb)
        {
            container = cosmosDb.GetContainer<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> ListAsync(string query)
        {
            FeedIterator<TEntity> feed = container.GetItemQueryIterator<TEntity>(new QueryDefinition(query));
            List<TEntity> results = new List<TEntity>();
            while (feed.HasMoreResults)
            {
                FeedResponse<TEntity> response = await feed.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return await container.CreateItemAsync(entity, new PartitionKey(entity.Id));
        }

        public async Task<TEntity> DeleteAsync(Guid key)
        {
            return await container.DeleteItemAsync<TEntity>(key.Format(), new PartitionKey(key.Format()));
        }

        public async Task<TEntity> GetAsync(Guid key)
        {
            try
            {
                ItemResponse<TEntity> response = await container.ReadItemAsync<TEntity>(key.Format(), new PartitionKey(key.Format()));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await container.UpsertItemAsync(entity, new PartitionKey(entity.Id));
        }
    }
}
