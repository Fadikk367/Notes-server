using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace notes.Models
{
    public class Note
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime CreatedAt { get; set; }
        [Required] public string Content { get; set; }
        [Required] public string Title { get; set; }
        [Required] public int SubjectId { get; set; }
        [JsonIgnore] public Subject Subject { get; set; }
    }
}
