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
                return LdapRepository.Identity;
            }
        }
    }
}
