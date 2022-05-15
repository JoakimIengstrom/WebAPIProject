using GeoComment.Data;
using GeoComment.Models;

namespace GeoComment.Services
{
    public class CommentHandler
    {
        private readonly GeoCommentDbContext _ctx;

        public CommentHandler(GeoCommentDbContext ctx)
        {  
            _ctx = ctx;
        }

        public Comment buildComment(NewComment input, User user)
        {
            var newComment = new Comment()
            {
                Author = user.UserName,
                Title = input.Body.title,
                Latitude = input.latitude,
                Longitude = input.longitude,
                Message = input.Body.message
            };
            return newComment;
        }

        public NewComment ReturnComment(Comment storedComment)
        {
            var comment = new NewComment()
            {
                Id = storedComment.Id,
                latitude = storedComment.Latitude,
                longitude = storedComment.Longitude,

                Body = new Body()
                {
                    title = storedComment.Title,
                    author = storedComment.Author,
                    message = storedComment.Message,
                }
            };
            return comment;
        }
    }
}
