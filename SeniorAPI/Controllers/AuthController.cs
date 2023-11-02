using Microsoft.AspNetCore.Mvc;
using SeniorAPI.Infraestrutura;
using SeniorAPI.Services;

namespace SeniorAPI.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        [Route("api/v1/auth")]
        public IActionResult Auth(string userName, string password)
        {
            var token = TokenService.GenerateToken(new User(userName, password));
            return Ok(token);
        }
    }
}
