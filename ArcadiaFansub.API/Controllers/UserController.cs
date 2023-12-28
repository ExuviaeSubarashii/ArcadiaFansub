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
    public class UserController(UserHandler userHandler) : ControllerBase
    {


        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] UserLoginRequest loginRequest)
        {
            return Ok(await userHandler.Login(loginRequest));
        }
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] CreateNewUserRequest registerRequest)
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
    }
}
