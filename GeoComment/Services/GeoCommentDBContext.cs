using GeoComment.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoComment.Services
{
    public class GeoCommentDBContext : DbContext
    {
        public GeoCommentDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Comment> Comments { get; set; }
    }
}
