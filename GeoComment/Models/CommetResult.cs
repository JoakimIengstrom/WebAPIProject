namespace GeoComment.Models
{
    public class CommentResult
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }

    //API V. 0.2

    public class ReturnCommentV0_2
    {
        public int id { get; set; }
        public ReturnBodyV0_2 body { get; set; }
        public int longitude { get; set; }
        public int latitude { get; set; }
    }
    public class ReturnBodyV0_2
    {
        public string author { get; set; }
        public string title { get; set; }
        public string message { get; set; }

    }
}