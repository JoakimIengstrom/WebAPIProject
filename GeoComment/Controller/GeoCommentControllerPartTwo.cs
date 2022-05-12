using System.ComponentModel.DataAnnotations;
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
    public class GeoCommentControllerPartTwo : ControllerBase
    {
        private readonly GeoCommentDbContext _ctx;
        private readonly UserManager<User> _userManager;
        private readonly CommentHandler _commentHandler;

        public GeoCommentControllerPartTwo(GeoCommentDbContext ctx, UserManager<User> userManager, CommentHandler commentHandler)
        {
            _ctx = ctx;
            _userManager = userManager;
            _commentHandler = commentHandler;
        }

        [ApiVersion("0.2")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Comment>> NewComment(
            NewComment input)
        {
            var user = await _userManager.GetUserAsync(User);

            var newComment = _commentHandler.buildComment(input, user);

            await _ctx.Comments.AddAsync(newComment);
            await _ctx.SaveChangesAsync();

            var storedComment = await _ctx.Comments.FirstAsync(sc =>
                sc.Title == newComment.Title &&
                sc.Author == newComment.Author &&
                sc.Message == newComment.Message &&
                sc.Latitude == newComment.Latitude &&
                sc.Longitude == newComment.Longitude);

            var comment = _commentHandler.ReturnComment(storedComment);
            
            return Created("", comment);
        }
        
        [HttpGet]
        [ApiVersion("0.2")]
        [Route("{id:int}")]
        public ActionResult<Comment> GetCommentFromId(int id)
        {
            if (id < 1 || id > _ctx.Comments.Count()) return NotFound();

            var commentFromId = _ctx.Comments.First(c => c.Id == id);

            var comment = _commentHandler.ReturnComment(commentFromId);
           
            return Ok(comment);
        }

        [HttpGet]
        [ApiVersion("0.2")]
        [Route("{username}")]
        public ActionResult<Array> GetCommentFromUser(string username)
        {
            var comments = _ctx.Comments
                .Where(c => c.Author == username).ToList();

            if (comments.Count == 0) return NotFound();

            var returnComments = new List<NewComment>();

            foreach (var usersComments in comments)
            {
                var comment = _commentHandler.ReturnComment(usersComments);
              
                returnComments.Add(comment);
            }
            return returnComments.ToArray();
        }

        [ApiVersion("0.2")]
        [HttpGet]
        public ActionResult<Array> GetCommentWithinRange(
            [Required] int minLon, [Required] int maxLon, [Required] int minLat, [Required] int maxLat)
        {
            var comments = _ctx.Comments
                .Where(c =>
                    c.Latitude >= minLat && c.Latitude <= maxLat &&
                    c.Longitude >= minLon && c.Longitude <= maxLon);

            var returnComments = new List<NewComment>();

            foreach (var usersComments in comments)
            {
                var comment = _commentHandler.ReturnComment(usersComments);
           
                returnComments.Add(comment);
            }
            return returnComments.ToArray();
        }
    }
}