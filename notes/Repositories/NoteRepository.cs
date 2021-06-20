using System.Collections;
using System.Collections.Generic;
using System.Linq;
using notes.Models;
using notes.Data;

namespace notes.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly NotesDbContext _ctx;

        public NoteRepository(NotesDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Note> GetAll()
        {
            return _ctx.Notes;
        }

        public IEnumerable<Note> GetAllBySubjectId(int subjectId)
        {
            return _ctx.Notes.Where(note => note.SubjectId == subjectId);
        }

        public Note GetOne(int id)
        {
            return _ctx.Notes.FirstOrDefault(note => note.Id == id);
        }

        public IEnumerable<Note> SearchByText(string searchQuery, int subjectId)
        {
            return _ctx.Notes.Where(x => x.Content.Contains(searchQuery) && x.SubjectId == subjectId);
        }

        public Note Add(Note toAdd)
        {
            _ctx.Notes.Add(toAdd);
            return toAdd;
        }

        public Note Update(Note toUpdate)
        {
            _ctx.Notes.Update(toUpdate);
            return toUpdate;
        }

        public void Delete(Note toDelete)
        {
            _ctx.Notes.Remove(toDelete);
        }

        public int Save()
        {
            return _ctx.SaveChanges();
        }
    }
}