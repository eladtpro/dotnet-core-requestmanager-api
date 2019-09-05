using Newtonsoft.Json;
using RequestManager.Extensions;
using System;

namespace RequestManager.Cosmos
{
    public abstract class Entity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get => Key.Format();
            set => Key = Guid.Parse(value);
        }

        public abstract Guid Key { get; set; }
    }
}


