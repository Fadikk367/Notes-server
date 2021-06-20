using System;
using Newtonsoft.Json;

namespace notes.IntegrationTests.Models
{
    [Serializable]
    class SubjectResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}