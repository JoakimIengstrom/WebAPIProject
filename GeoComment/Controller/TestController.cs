using GeoComment.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoComment.Controller
{
    [ApiVersion("0.1")]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly DatabaseHandler _databaseHandler;

        public TestController(DatabaseHandler databaseHandler)
        {
            _databaseHandler = databaseHandler;
        }

        [Route("reset-db")]
        [HttpGet]
        public async Task<IActionResult> ResetDB()
        {
            await _databaseHandler.CreateDB();
            return Ok();
        }
    }
}