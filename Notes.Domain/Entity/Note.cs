using System.Drawing;

namespace Notes.Domain.Entity
{
    public class Note : BaseEntity
    {
        public string Title { get; set; } = String.Empty;
        //public Content? Content { get; set; }
        public Guid UserId { get; set; }
        public Guid NoteId { get; set; }
       // public Color BackGround { get; set; }
    }
}