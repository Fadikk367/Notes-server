using System;
using Newtonsoft.Json;

namespace notes.IntegrationTests.Models
{
    [Serializable]
    class NoteResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("subjectId")]
        public int SubjectId { get; set; }
    }
}