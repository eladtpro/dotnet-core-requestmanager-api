using Newtonsoft.Json;
using RequestManager.Extensions;
using System;

namespace RequestManager.Cosmos
{
    public abstract class Entity
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        public abstract Guid Key { get; set; }
    }
}


