using Newtonsoft.Json;
using RequestManager.Cosmos;
using System;
using System.ComponentModel;
using System.Security.Principal;

namespace RequestManager.Model
{
    public class User : Entity, IIdentity
    {

        [JsonProperty("key")]
        public override Guid Key { get; set; }
        [Description("givenName")]
        [JsonProperty("firstName")]
        public string FirstName { get; internal set; }
        [Description("sn")]
        [JsonProperty("lastName")]
        public string LastName { get; internal set; }
        [Description("displayName")]
        [JsonProperty("displayName")]
        public string DisplayName { get; internal set; }
        [Description("mail")]
        [JsonProperty("email")]
        public string Email { get; internal set; }
        [Description("sAMAccountName")]
        [JsonProperty("username")]

        public string Username { get; internal set; }
        public string AuthenticationType { get; internal set; }
        public bool IsAuthenticated { get; internal set; }
        public string Name { get; internal set; }
        public string Domain { get; internal set; }


    }
}
