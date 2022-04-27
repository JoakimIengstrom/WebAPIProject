namespace GeoComment.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

    }
}
