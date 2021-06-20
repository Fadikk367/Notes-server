using notes.Models;
using Microsoft.EntityFrameworkCore;

namespace notes.Data
{
    public class NotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) {}
    }
}
 