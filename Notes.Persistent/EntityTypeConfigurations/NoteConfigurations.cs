using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Entity;

namespace Notes.Persistence.EntityTypeConfigurations
{
    public class NoteConfigurations : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(e => e.NoteId);
            builder.Property(e => e.Title).IsRequired();
            
        }
    }
}
