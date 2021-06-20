using System.Collections.Generic;
using notes.Models;

namespace notes.Repositories
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetAll();
        IEnumerable<Note> GetAllBySubjectId(int subjectId);
        Note GetOne(int id);
        IEnumerable<Note> SearchByText(string searchQuery, int subjectId);
        Note Add(Note toAdd);
        void Delete(Note toDelete);
        int Save();
    }
}