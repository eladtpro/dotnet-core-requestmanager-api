using RequestManager.Cosmos;
using RequestManager.Model;

namespace RequestManager.Services
{
    public class RequestService : CosmosDbService<Request>
    {
        public RequestService(CosmosDb cosmosDb) : base(cosmosDb)
        {

        }
    }
}
