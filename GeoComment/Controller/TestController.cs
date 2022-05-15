using GeoComment.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoComment.Controller
{
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly DatabaseHandler _databaseHandler;

        public TestController(DatabaseHandler databaseHandler)
        {
            _databaseHandler = databaseHandler;
        }

        [ApiVersion("0.1")]
        [ApiVersion("0.2")] 
        [Route("reset-db")]
        [HttpGet]
        public async Task<IActionResult> ResetDB()
        {
            await _databaseHandler.CreateDB();
            return Ok();
        }
    }
}