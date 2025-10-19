using Hng0Task.Service;
using Microsoft.AspNetCore.Mvc;

namespace Hng0Task.Controllers
{
    [ApiController]
    [Route("")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;

        public ProfileController()
        {
            _profileService = new ProfileService();
        }

        [HttpGet("/me")]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _profileService.GetProfileAsync();
            return Ok(profile);
        }
    }
}
