using GeoComment.DTO;
using GeoComment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GeoComment.Controller
{
    [Route("api/user")]
    [ApiVersion("0.2")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(NewUser newUser)
        {
            var user = new User()
            {
                UserName = newUser.Username
            };

            await _userManager.CreateAsync(user, newUser.Password);

            var registrationSuccess =
                await _userManager.CheckPasswordAsync(user,
                    newUser.Password);

            if (!registrationSuccess)
            {
                return BadRequest(); //statuscode 400
            }

            var createdUser =
                await _userManager.FindByNameAsync(user.UserName);
            return Created("",
                new
                {
                    username = createdUser.UserName, id = createdUser.Id
                });

        }

        [Route("login")]
        [HttpPost]
        public ActionResult newUser(userData newUser)
        {
            return Ok(newUser);
        }
    }
}
