using System;
using System.ComponentModel.DataAnnotations;


namespace notes.Models.DTO
{
    public class NoteDTO
    {
        [Required] public string Content { get; set; }
        [Required] public string Title { get; set; }
        [Required] public int SubjectId { get; set; }
    }
}
