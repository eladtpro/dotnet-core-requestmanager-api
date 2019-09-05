using Microsoft.AspNetCore.Mvc;
using RequestManager.Cosmos;
using RequestManager.Model;
using RequestManager.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RequestManager.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : Controller
    {
        private readonly RequestService service;

        public RequestController(CosmosDb cosmosDb)
        {
            service = new RequestService(cosmosDb);
        }

        // GET api/requests
        [HttpGet]
        public async Task<IEnumerable<Request>> List(RequestStatus? status, PackageType? type, string pattern)
        {
            StringBuilder query = new StringBuilder("SELECT * FROM Request r WHERE 1=1 ");

            if (null != status)
                query.AppendLine($"AND r.status = {status}");
            if (null != type)
                query.AppendLine($"AND r.type = {type}");
            if (null != pattern)// TODO - add Package.Name to query
                query.AppendLine($"AND ( CONTAINS(r.user, '{pattern}') OR  CONTAINS(r.email, '{pattern}') )");
            
            return await service.ListAsync(query.ToString());
        }

        // GET api/requests/523486
        [HttpGet("{key}")]
        public async Task<Request> Get(Guid key)
        {
            return await service.GetAsync(key);
        }

        // POST api/requests
        [HttpPost]
        public async Task<Request> Post([FromBody]Request request)
        {
            if (request.Key == Guid.Empty)
                request.Key = Guid.NewGuid();

            return await service.AddAsync(request);
        }

        // PUT api/requests
        [HttpPut("{key}")]
        public async Task<Request> Put([FromBody]Request request)
        {
            return await service.UpdateAsync(request);
        }

        // DELETE api/requests/5
        [HttpDelete("{key}")]
        public async Task<Request> Delete(Guid key)
        {
            return await service.DeleteAsync(key);
        }
    }
}
