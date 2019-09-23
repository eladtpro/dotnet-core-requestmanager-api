using System;
using Newtonsoft.Json;
using RequestManager.Cosmos;

namespace RequestManager.Model
{
    public class Request : Entity, IEquatable<Request>
    {
        [JsonProperty("key")]
        public override Guid Key { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("package")]
        public Package Package { get; set; }
        [JsonProperty("submittedOn")]
        public DateTime SubmittedOn { get; set; }
        [JsonProperty("status")]
        public RequestStatus Status { get; set; }
        [JsonProperty("distribution")]
        public DistributionType Distribution { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Request request = obj as Request;
            return Equals(request);
        }
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
        public bool Equals(Request other)
        {
            if (other == null) return false;
            return (Key.Equals(other.Key));
        }

        public override string ToString()
        {
            return $"Key: {Key}, User: {User}, Status: {Status}, Package: {Package}, SubmittedOn: {SubmittedOn}, Distribution: {Distribution}";
        }
    }
}