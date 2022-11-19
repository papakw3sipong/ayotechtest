using aYoTechTest.BR.Services.Interfaces;
using aYoTechTest.BR.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aYoTechTest.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class IdentityController : Controller
    {
        private IIdentityUserService _identityService;

        public IdentityController(IIdentityUserService identityService)
        {
            _identityService = identityService;
        }


        [HttpGet]
        [AllowAnonymous]
        public JsonResult Get()
        {
            return new JsonResult(new[] { "Welcome to Unit Conversion Api." });
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> AuthenticateUser([FromBody] ApiAuthRequest model)
        {
            var _result = await _identityService.AuthenticateUser(model);

            if (_result == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(_result);
        }
    }
}