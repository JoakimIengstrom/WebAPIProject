using GeoComment.Data;
using GeoComment.Models;
using GeoComment.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoComment.Controller
{
    [Route("api/geo-comments")]
    [ApiController]
    public class GeoCommentController : ControllerBase
    {
        private readonly GeoCommentDbContext _ctx;
        private readonly DatabaseHandler _databaseHandler;

        public GeoCommentController(GeoCommentDbContext ctx, DatabaseHandler databaseHandler)
        {
            _ctx = ctx;
            _databaseHandler = databaseHandler;
        }

        [HttpPost]
        public async Task <ActionResult<CommentResult>> NewComment(CommentInput input)
        {
            var newComment = new CommentResult()
            {
                Message = input.Message,
                Author = input.Author,
                Longitude = input.Longitude,
                Latitude = input.Latitude
            };

            await _ctx.Comments.AddAsync(newComment);
            await _ctx.SaveChangesAsync();

            return Created("", newComment);
        }
    }
}