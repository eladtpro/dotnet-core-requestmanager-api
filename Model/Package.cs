using Newtonsoft.Json;

namespace RequestManager.Model
{
    public class Package
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("type")]
        public PackageType Type { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Type: {Type}";
        }
    }
}