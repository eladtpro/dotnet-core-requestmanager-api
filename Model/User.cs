using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace RequestManager.Model
{
    public class User : IIdentity
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }

        public string AuthenticationType => throw new NotImplementedException();
        public bool IsAuthenticated => throw new NotImplementedException();
        public string Name => throw new NotImplementedException();
    }
}
