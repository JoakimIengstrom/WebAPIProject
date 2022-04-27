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

        [Route("reset-db")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetDB()
        {
            await _databaseHandler.ResetDB();
            return Ok();
        }
    }
}