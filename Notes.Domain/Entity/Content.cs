namespace Notes.Domain.Entity
{
    public class Content : BaseEntity
    {
        public List<Image>? Images { get; set; }
        public string? Text { get; set; }
    }
}