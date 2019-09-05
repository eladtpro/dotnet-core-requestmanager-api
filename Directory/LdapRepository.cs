using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Novell.Directory.Ldap;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Microsoft.Extensions.DependencyInjection;

using User = RequestManager.Model.User;


namespace RequestManager.Directory
{
    public class LdapRepository
    {
        private static readonly Lazy<string[]> Attributes = new Lazy<string[]>(() =>
        {
            DescriptionAttribute[] attrs = Attribute.GetCustomAttributes(typeof(User)).Cast<DescriptionAttribute>().ToArray();
            return attrs.Select(attr => attr.Description).ToArray();
        });

        public static User Identity
        {
            get
            {
                IMemoryCache cache = HttpContext.RequestServices.GetService<IMemoryCache>();
                string fqdn = HttpContext.User.Identity.Name;

                User user;
                if (cache.TryGetValue(fqdn, out user))
                    return user;
                user = Get(HttpContext.User.Identity);
                cache.Set(fqdn, user);
                return user;

            }
        }

        private static IHttpContextAccessor httpContextAccessor;
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }
        private static HttpContext HttpContext => httpContextAccessor.HttpContext;
        private static User Get(IIdentity identity)
        {
            string[] fqdn = identity.Name.Split('\\');
            User user = new User
            {
                Name = fqdn.Last(),
                Domain = fqdn.First(),
                AuthenticationType = identity.AuthenticationType,
                IsAuthenticated = identity.IsAuthenticated
            };

            using (LdapConnection ldapConnection = new LdapConnection() { SecureSocketLayer = false })
            {
                ldapConnection.Connect("Server", 0);
                ldapConnection.Bind(LdapConnectionSettings.Current.User, LdapConnectionSettings.Current.Password);
                string filter = string.Format("UserFilter", user.Name);

                LdapSearchResults results = ldapConnection.Search(
                    "SearchBase",
                    LdapConnection.SCOPE_SUB,
                    filter,
                    Attributes.Value,
                    false);

                LdapEntry entry = results.HasMore() ? results.Next() : null;
                if (null == entry)
                    return user;

                Fill(ref user, entry);
                return user;
            }
        }
        private static void Fill(ref User user, LdapEntry entry)
        {
            PropertyInfo[] props = typeof(User).GetProperties();
            foreach (PropertyInfo property in props)
            {
                DescriptionAttribute propertyAttr = property.GetCustomAttribute<DescriptionAttribute>(false);
                if (null == propertyAttr)
                    continue;
                LdapAttribute ldapAttr = entry.getAttribute(propertyAttr.Description);
                if (null == ldapAttr)
                    continue;

                string value = ldapAttr.StringValue;
                property.SetValue(user, value);
            }
        }
    }
}
