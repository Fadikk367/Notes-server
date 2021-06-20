using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace notes.Models
{
    public class Subject
    {
        [Key] 
        [JsonProperty("Id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonIgnore] 
        public List<Note> Notes { get; set; }
    }
}
