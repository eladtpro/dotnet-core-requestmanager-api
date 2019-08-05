using Microsoft.AspNetCore.Mvc;
using RequestManager.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RequestManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private static readonly Lazy<ConcurrentDictionary<int, Request>> requests = new Lazy<ConcurrentDictionary<int, Request>>(
            () => new ConcurrentDictionary<int, Request>(Dummy.Requests),
            LazyThreadSafetyMode.PublicationOnly);

        public static ConcurrentDictionary<int, Request> Requests => requests.Value;
        
        // GET api/requests
        [HttpGet]
        public IEnumerable<Request> Get(RequestStatus? status, PackageType? type, string pattern)
        {
            //return Requests.
            List<Request> result = Requests.Values.Where(request =>
            {
                bool s = (null == status) ? true : status == request.Status;
                bool t = (null == type) ? true : type == request.Package.Type;
                bool p = (string.IsNullOrWhiteSpace(pattern)) ? true : request.Match(pattern);
                bool match = s && t && p;
                //(null == status) ? true : status == request.Status &&
                //(null == type) ? true : type == request.Package.Type &&
                //(null == pattern) ? true : request.Match(pattern);
                return match;
            }).ToList();
            return result.Distinct();
        }

        // GET api/requests/523486
        [HttpGet("{id}")]
        public Request Get(int id)
        {
            return Requests.TryGetValue(id, out Request existing) ? existing : null;
        }


        // POST api/requests
        [HttpPost]
        public void Post([FromBody] Request request)
        {
            Requests.TryAdd(request.Id, request);
        }

        // PUT api/requests
        public Request Put([FromBody]Request request)
        {
            request.Id = Requests.Keys.Max() + 1;
            Requests[request.Id] = request;
            return Requests[request.Id];
        }

        // DELETE api/requests/5
        public Request Delete(int id)
        {
            Requests.TryRemove(id, out Request request);
            return request;
        }
    }
}
