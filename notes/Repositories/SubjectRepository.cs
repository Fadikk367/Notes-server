using System.Collections;
using System.Collections.Generic;
using System.Linq;
using notes.Models;
using notes.Data;
using System.Threading.Tasks;

namespace notes.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly NotesDbContext _ctx;

        public SubjectRepository(NotesDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Subject> GetAll()
        {
            return _ctx.Subjects;
        }

        public async Task<Subject> GetOne(int id)
        {
            var subject = _ctx.Subjects.FirstOrDefault(x => x.Id == id);

            await _ctx.Entry(subject).Collection(t => t.Notes).LoadAsync();

            return subject;
        }

        public Subject Add(Subject toAdd)
        {
            _ctx.Subjects.Add(toAdd);
            return toAdd;
        }

        public Subject Update(Subject toUpdate)
        {
            _ctx.Subjects.Update(toUpdate);
            return toUpdate;
        }

        public void Delete(Subject subjectToDelete)
        {
            _ctx.Subjects.Remove(subjectToDelete);
        }

        public int Save()
        {
            return _ctx.SaveChanges();
        }
    }
}