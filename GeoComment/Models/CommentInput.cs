namespace GeoComment.Models
{
    public class CommentInput
    {
        public string Message { get; set; }
        public string Author { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }

    //API V. 0.2

    public class NewCommentV0_2
    {
        public NewCommentBodyV0_2 Body { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
    }
    public class NewCommentBodyV0_2 {
      
        public string Title { get; set; }
        public string Message { get; set; }
    }
}