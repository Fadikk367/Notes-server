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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly INoteRepository _notesRepository;

        public SubjectController(ISubjectRepository subjectRepository, INoteRepository notesRepository)
        {
            _subjectRepository = subjectRepository;
            _notesRepository = notesRepository;
        }

        [HttpGet]
        public IEnumerable<Subject> GetAll()
        {
            return _subjectRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Task<Subject> GetById(int id)
        {
            return _subjectRepository.GetOne(id);
        }


        [HttpGet("{id}/Notes")]
        public IEnumerable<Note> GetNotesBySubject(
            [FromQuery(Name = "search")] string searchQuery,
            [FromRoute(Name = "id")] int id
        )
        {
            if (searchQuery == null || searchQuery.Trim().Length == 0)
            {
                return _notesRepository.GetAllBySubjectId(id);
            }
            else
            {
                return _notesRepository.SearchByText(searchQuery, id);
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] SubjectDTO subjectDTO)
        {
            try
            {
                if (subjectDTO == null)
                    return BadRequest();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var subject = new Subject{ 
                    Name = subjectDTO.Name,
                    Description = subjectDTO.Description
                };

                _subjectRepository.Add(subject);

                var save = _subjectRepository.Save();

                if (save > 0)
                {
                    return Ok(subject);
                }

                return BadRequest();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var subject = await _subjectRepository.GetOne(id);

                if (subject == null) return BadRequest();

                _subjectRepository.Delete(subject);

                var save = _subjectRepository.Save();

                if (save > 0) return Ok();

                return BadRequest();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }
    }
}
