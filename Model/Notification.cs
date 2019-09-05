using Newtonsoft.Json;
using RequestManager.Cosmos;
using System;

namespace RequestManager.Model
{
    public class Notification : Entity
    {
        [JsonProperty("key")]
        public override Guid Key { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("sender")]
        public string Sender { get; set; }
        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }
        [JsonProperty("severity")]
        public NotificationSeverity Severity { get; set; }
    }
}
