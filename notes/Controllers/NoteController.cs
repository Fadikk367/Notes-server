using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;
using notes.Models;
using notes.Repositories;
using notes.Models.DTO;

namespace notes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet("{id}")]
        public Note GetById(int id)
        {
            return _noteRepository.GetOne(id);
        }

        [HttpPost]
        public IActionResult Insert([FromBody] NoteDTO noteDTO)
        {
            try
            {
                if (noteDTO == null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var subject = new Note
                {
                    Title = noteDTO.Title,
                    Content = noteDTO.Content,
                    CreatedAt = DateTime.Now,
                    SubjectId = noteDTO.SubjectId
                };

                _noteRepository.Add(subject);

                var save = _noteRepository.Save();

                if (save > 0)
                {
                    return Ok(subject);
                }

                return BadRequest();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var subject = _noteRepository.GetOne(id);

                if (subject == null) return BadRequest();

                _noteRepository.Delete(subject);

                var save = _noteRepository.Save();

                if (save > 0) return Ok();

                return BadRequest();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
