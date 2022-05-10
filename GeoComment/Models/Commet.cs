namespace GeoComment.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }

    public class NewComment
    {
        public int Id { get; set; }
        public Body Body { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
    }
    public class Body
    {
        public string title { get; set; }
        public string? author { get; set; }
        public string message { get; set; }
    }
}