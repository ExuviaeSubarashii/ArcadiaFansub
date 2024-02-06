using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos;
using ArcadiaFansub.Domain.RequestDtos.UserRequest;
using ArcadiaFansub.Services.Services.AnimeServices;
using ArcadiaFansub.Services.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserHandler userHandler, UserAuthentication authHandler) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            return Ok(await userHandler.Login(loginRequest));
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateNewUserRequest registerRequest)
        {
            if (registerRequest != null)
            {
                return Ok(await userHandler.CreateUser(registerRequest));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("IsAdmin")]
        public async Task<IActionResult> IsAdminAuthenticate([FromBody] UserAuthRequest request)
        {
            return Ok(await authHandler.IsAdmin(request.UserToken));
        }
        [HttpPost("AuthUser")]
        public async Task<IActionResult> AuthUser([FromBody] UserAuthRequest request)
        {
            return Ok(await authHandler.AuthUser(request.UserToken));
        }
    }
}
