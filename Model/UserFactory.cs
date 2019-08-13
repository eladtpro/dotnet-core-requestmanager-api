using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading.Tasks;

namespace RequestManager.Model
{
    public partial class User
    {
        private static Lazy<string[]> Attributes = new Lazy<string[]>(() =>
        {
            DescriptionAttribute[] attrs = Attribute.GetCustomAttributes(typeof(User)).Cast<DescriptionAttribute>().ToArray();
            return attrs.Select(attr => attr.Description).ToArray();
        });


        public static User Factory(IIdentity identity)
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
                ldapConnection.Connect(Properties.Resources.Server, int.Parse(Properties.Resources.Port));
                try
                {
                    ldapConnection.Bind(Properties.Resources.User, Properties.Resources.Password);
                    string filter = string.Format(Properties.Resources.UserFilter, user.Name);

                    LdapSearchResults results = ldapConnection.Search(
                        Properties.Resources.SearchBase,
                        LdapConnection.SCOPE_SUB,
                        filter,
                        User.Attributes.Value,
                        false);

                    LdapEntry entry = results.HasMore() ? results.Next() : null;
                    if (null == entry)
                        return user;

                    user.Fill(entry);
                    return user;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Fill(LdapEntry entry)
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
                property.SetValue(this, value);
            }
        }
    }
}
