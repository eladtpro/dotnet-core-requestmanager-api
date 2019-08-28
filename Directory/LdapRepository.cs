using Novell.Directory.Ldap;
using RequestManager.Model;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Principal;

namespace RequestManager.Directory
{
    public class LdapRepository
    {
        private static Lazy<string[]> Attributes = new Lazy<string[]>(() =>
        {
            DescriptionAttribute[] attrs = Attribute.GetCustomAttributes(typeof(User)).Cast<DescriptionAttribute>().ToArray();
            return attrs.Select(attr => attr.Description).ToArray();
        });

        public static User Fill(IIdentity identity)
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
                try
                {
                    //string user = Configuration["Movies:ServiceApiKey"]

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
                catch (Exception)
                {
                    throw;
                }
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
