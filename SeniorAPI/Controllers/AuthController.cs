using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SeniorAPI.Models;
using SeniorAPI.Services;

namespace SeniorAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("api/v1/auth")]
        public IActionResult Auth(string userName, string password)
        {
            try
            {
                if (!userName.IsNullOrEmpty() && !password.IsNullOrEmpty())
                {
                    var token = TokenService.GenerateToken(new UserModel(userName, password));
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
