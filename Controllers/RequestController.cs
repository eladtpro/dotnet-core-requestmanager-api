using Microsoft.AspNetCore.Mvc;
using RequestManager.Model;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RequestManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : Controller
    {
        public static IDictionary<int, Request> Requests => Dummy.Requests;

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
        public Request Post([FromBody]Request request)
        {
            request.Id = Requests.Keys.Max() + 1;
            request.User = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (Requests.TryAdd(request.Id, request))
                return request;
            return null;
        }

        // PUT api/requests
        [HttpPut("{id}")]
        public Request Put([FromBody]Request request)
        {
            if (request.Id < 1)
                request.Id = Requests.Keys.Max() + 1;

            Requests[request.Id] = request;

            return request;
        }

        // DELETE api/requests/5
        [HttpDelete("{id}")]
        public Request Delete(int id)
        {
            if(Requests.TryGetValue(id, out Request request))
                Requests.Remove(id);
            return request;
        }
    }
}
