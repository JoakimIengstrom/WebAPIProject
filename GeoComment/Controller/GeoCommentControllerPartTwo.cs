using GeoComment.Data;
using GeoComment.Models;
using GeoComment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoComment.Controller
{
    
    [Route("api/geo-comments")]
    [ApiController]
    public class GeoCommentControllerPartTWo : ControllerBase
    {
        private readonly GeoCommentDbContext _ctx;
        private readonly UserManager<User> _userManager;

        public GeoCommentControllerPartTWo(GeoCommentDbContext ctx, UserManager<User> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;

        }

        [ApiVersion("0.2")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CommentResult>> NewComment(
            NewCommentV0_2 input)
        {
            var user = await _userManager.GetUserAsync(User);

            var newComment = new CommentResult()
            {
                Author = user.UserName,
                Title = input.Body.Title,
                Latitude = input.Latitude,
                Longitude = input.Longitude,
                Message = input.Body.Message
            };

            await _ctx.Comments.AddAsync(newComment);
            await _ctx.SaveChangesAsync();

            var inputComment = await _ctx.Comments.FirstAsync(ic =>
                ic.Title == newComment.Title &&
                ic.Author == newComment.Author &&
                ic.Message == newComment.Message &&
                ic.Latitude == newComment.Latitude &&
                ic.Longitude == newComment.Longitude);

            var addedComment = new ReturnCommentV0_2()
            {
                id = inputComment.Id,
                latitude = inputComment.Longitude,
                longitude = inputComment.Longitude,
                
                body = new ReturnBodyV0_2()
                {
                    title = inputComment.Title,
                    author = inputComment.Author,
                    message = inputComment.Message,
                }
            };

            return Created("", addedComment);
        }

        [ApiVersion("0.2")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetCommentTaskFromId(int id)
        {
            var comment = await _ctx.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null) return NotFound(); //statuscode 404
            
            return Ok(comment);
        }

        [ApiVersion("0.2")]
        [HttpGet]
        public ActionResult<Array> GetCommentWithinValues(
            int? minLon, int? maxLon, int? minLat, int? maxLat)
        {
            var comment = _ctx.Comments
                .Where(c =>
                    c.Latitude >= minLat && c.Latitude <= maxLat &&
                    c.Longitude >= minLon && c.Longitude <= maxLon).ToArray();

            if (comment.Length == 0) return BadRequest(); //statuscode 400

            return Ok(comment);
        }
    }
}