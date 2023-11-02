using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            try
            {
                if (!userName.IsNullOrEmpty() && !password.IsNullOrEmpty())
                {
                    var token = TokenService.GenerateToken(new User(userName, password));
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Login or password invalid.");
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }
    }
}
