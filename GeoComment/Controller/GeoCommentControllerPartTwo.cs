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

        public GeoCommentControllerPartTwo(GeoCommentDbContext ctx, UserManager<User> userManager)
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
                latitude = inputComment.Latitude,
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

        [HttpGet]
        [ApiVersion("0.2")]
        [Route("{id:int}")]
        public ActionResult<CommentResult> GetCommentFromId(int id)
        {
            if (id < 1 || id > _ctx.Comments.Count()) return NotFound();

            var commentFromId = _ctx.Comments.First(c => c.Id == id);

            var comment = new ReturnCommentV0_2()
            {
                id = commentFromId.Id,
                latitude = commentFromId.Latitude,
                longitude = commentFromId.Longitude,
                body = new ReturnBodyV0_2()
                {
                    title = commentFromId.Title,
                    author = commentFromId.Author,
                    message = commentFromId.Message
                }
            };

            return Ok(comment);
        }

        [HttpGet]
        [ApiVersion("0.2")]
        [Route("{username}")]
        public ActionResult<Array> GetCommentFromUser(string username)
        {
            var commentsFromUser = _ctx.Comments
                .Where(c => c.Author == username).ToList();

            if (commentsFromUser.Count == 0) return NotFound();

            var returnComments = new List<ReturnCommentV0_2>();

            foreach (var usersComments in commentsFromUser)
            {
                var comments = new ReturnCommentV0_2()
                {
                    id = usersComments.Id,
                    latitude = usersComments.Latitude,
                    longitude = usersComments.Longitude,
                    body = new ReturnBodyV0_2()
                    {
                        title = usersComments.Title,
                        author = usersComments.Author,
                        message = usersComments.Message
                    }
                };
                returnComments.Add(comments);
            }
            return returnComments.ToArray();
        }
        
        [ApiVersion("0.2")]
        [HttpGet]
        public ActionResult<Array> GetCommentWithinRange(
            int? minLon, int? maxLon, int? minLat, int? maxLat)
        {
            var comments = _ctx.Comments
                .Where(c =>
                    c.Latitude >= minLat && c.Latitude <= maxLat &&
                    c.Longitude >= minLon && c.Longitude <= maxLon).ToList();

            if (comments.Count == 0) return BadRequest();

            var returnComments = new List<ReturnCommentV0_2>();
            

            foreach (var usersComments in comments)
            {
                var comment = new ReturnCommentV0_2()
                {
                    id = usersComments.Id,
                    latitude = usersComments.Latitude,
                    longitude = usersComments.Longitude,
                    body = new ReturnBodyV0_2()
                    {
                        title = usersComments.Title,
                        author = usersComments.Author,
                        message = usersComments.Message
                    }
                };
                returnComments.Add(comment);
            }
            return returnComments.ToArray();
        }
    }
}