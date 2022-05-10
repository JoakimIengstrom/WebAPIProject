using System.ComponentModel.DataAnnotations;
using GeoComment.Data;
using GeoComment.Models;
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
        public async Task<ActionResult<Comment>> NewComment(
            NewComment input)
        {
            var user = await _userManager.GetUserAsync(User);

            var newComment = new Comment()
            {
                Author = user.UserName,
                Title = input.Body.title,
                Latitude = input.latitude,
                Longitude = input.longitude,
                Message = input.Body.message
            };

            await _ctx.Comments.AddAsync(newComment);
            await _ctx.SaveChangesAsync();

            var inputComment = await _ctx.Comments.FirstAsync(ic =>
                ic.Title == newComment.Title &&
                ic.Author == newComment.Author &&
                ic.Message == newComment.Message &&
                ic.Latitude == newComment.Latitude &&
                ic.Longitude == newComment.Longitude);

            var comment = new NewComment()
            {
                Id = inputComment.Id,
                latitude = inputComment.Latitude,
                longitude = inputComment.Longitude,

                Body = new Body()
                {
                    title = inputComment.Title,
                    author = inputComment.Author,
                    message = inputComment.Message,
                }
            };

            return Created("", comment);
        }
        
        [HttpGet]
        [ApiVersion("0.2")]
        [Route("{id:int}")]
        public ActionResult<Comment> GetCommentFromId(int id)
        {
            if (id < 1 || id > _ctx.Comments.Count()) return NotFound();

            var commentFromId = _ctx.Comments.First(c => c.Id == id);

            var comment = new NewComment()
            {
                Id = commentFromId.Id,
                latitude = commentFromId.Latitude,
                longitude = commentFromId.Longitude,
                Body = new Body()
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
            var comments = _ctx.Comments
                .Where(c => c.Author == username).ToList();

            if (comments.Count == 0) return NotFound();

            var returnComments = new List<NewComment>();

            foreach (var usersComments in comments)
            {
                var comment = new NewComment()
                {
                    Id = usersComments.Id,
                    latitude = usersComments.Latitude,
                    longitude = usersComments.Longitude,
                    Body = new Body()
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
                var comment = new NewComment()
                {
                    Id = usersComments.Id,
                    latitude = usersComments.Latitude,
                    longitude = usersComments.Longitude,
                    Body = new Body()
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