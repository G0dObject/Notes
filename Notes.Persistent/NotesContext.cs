using Microsoft.EntityFrameworkCore;

using Notes.Application.Interfaces;
using Notes.Domain.Entity;
using Notes.Persistence.EntityTypeConfigurations;


namespace Notes.Persistence
{
    public class NotesContext : DbContext, INotesDbContext
    {
        public DbSet<Note>? Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteConfigurations());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=notes;Uid=Admin;Pwd=nzBpnN!qCL98tdj;");
        }

    }
}
