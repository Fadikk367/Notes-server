using System.Collections.Generic;
using System.Threading.Tasks;
using notes.Models;

namespace notes.Repositories
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> GetAll();
        Task<Subject> GetOne(int id);
        Subject Add(Subject subject);
        Subject Update(Subject subject);
        void Delete(Subject subject);
        int Save();
    }
}