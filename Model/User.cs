using System.ComponentModel;
using System.Security.Principal;

namespace RequestManager.Model
{
    public class User : IIdentity
    {
        internal User() { }

        public string AuthenticationType { get; internal set; }
        public bool IsAuthenticated { get; internal set; }
        public string Name { get; internal set; }
        public string Domain { get; internal set; }

        [Description("givenName")]
        public string FirstName { get; internal set; }
        [Description("sn")]
        public string LastName { get; internal set; }
        [Description("displayName")]
        public string DisplayName { get; internal set; }
        [Description("mail")]
        public string Email { get; internal set; }
        [Description("sAMAccountName")]
        public string Username { get; internal set; }

    }
}
