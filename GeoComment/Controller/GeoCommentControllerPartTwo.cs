﻿using GeoComment.Data;
using GeoComment.Models;
using GeoComment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeoComment.Controller
{
    
    [Route("api/geo-comments")]
    [ApiController]
    public class GeoCommentControllerPartTWo : ControllerBase
    {
        private readonly GeoCommentDbContext _ctx;
        private readonly DatabaseHandler _databaseHandler;

        public GeoCommentControllerPartTWo(GeoCommentDbContext ctx, DatabaseHandler databaseHandler)
        {
            _ctx = ctx;
            _databaseHandler = databaseHandler;
        }

        [ApiVersion("0.2")]
        [HttpPost]
        [Authorize]
        public async Task <ActionResult<CommentResult>> NewComment(CommentInput input)
        {
            var newComment = new CommentResult()
            {
                Author = input.Author,
                Message = input.Message,
                Latitude = input.Latitude,
                Longitude = input.Longitude
            };

            await _ctx.Comments.AddAsync(newComment);
            await _ctx.SaveChangesAsync();

            return Created("", newComment);
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