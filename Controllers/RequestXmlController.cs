//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestManager.Cosmos;

namespace RequestManager.Controllers
{
    //[Authorize]
    [Produces("application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestXmlController : RequestController
    {
        public RequestXmlController(CosmosDb cosmosDb) : base(cosmosDb){ }
    }
}
