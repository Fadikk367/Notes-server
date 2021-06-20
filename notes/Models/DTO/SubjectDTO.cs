using System;
using System.ComponentModel.DataAnnotations;


namespace notes.Models.DTO
{
    public class SubjectDTO
    {
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
    }
}
