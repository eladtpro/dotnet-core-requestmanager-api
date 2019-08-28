using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using RequestManager.Directory;
using User = RequestManager.Model.User;

namespace RequestManager.Controllers
{
    public abstract class Controller : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        protected User Identity
        {
            get
            {
                User user;
                IMemoryCache cache = HttpContext.RequestServices.GetService<IMemoryCache>();
                string fqdn = HttpContext.User.Identity.Name;

                if (cache.TryGetValue(fqdn, out user))
                    return user;
                user = LdapRepository.Fill(HttpContext.User.Identity);
                cache.Set(fqdn, user);
                return user;
            }
        }
    }
}
