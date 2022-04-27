namespace GeoComment.Services
{
    public class DatabaseHandler
    {
        private readonly GeoCommentDBContext _ctx;

        public DatabaseHandler(GeoCommentDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task ResetDB()
        {
            await CreateDB();
            await Seed();
        }

        public async Task CreateDB()
        {
            await _ctx.Database.EnsureDeletedAsync();
            await _ctx.Database.EnsureCreatedAsync();
        }

        public async Task Seed()
        {

        }




    }
}
