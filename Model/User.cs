using System.ComponentModel;
using System.Security.Principal;

namespace RequestManager.Model
{
    public partial class User : IIdentity
    {
        private User() { }

        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string Name { get; private set; }
        public string Domain { get; private set; }

        [Description("givenName")]
        public string FirstName { get; private set; }
        [Description("sn")]
        public string LastName { get; private set; }
        [Description("displayName")]
        public string DisplayName { get; private set; }
        [Description("mail")]
        public string Email { get; private set; }
        [Description("sAMAccountName")]
        public string Username { get; private set; }

    }
}
