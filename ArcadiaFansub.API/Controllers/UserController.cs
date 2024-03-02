using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos;
using ArcadiaFansub.Domain.RequestDtos.UserRequest;
using ArcadiaFansub.Services.Services.AnimeServices;
using ArcadiaFansub.Services.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserHandler userHandler, UserAuthentication authHandler) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest, CancellationToken cancellationToken)
        {
            return await(userHandler.Login(loginRequest, cancellationToken)) is { } result ? Ok(result) : BadRequest(); ;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateNewUserRequest registerRequest, CancellationToken cancellationToken)
        {
            if (registerRequest != null)
            {
                return Ok(await userHandler.CreateUser(registerRequest, cancellationToken));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("IsAdmin")]
        public async Task<IActionResult> IsAdminAuthenticate([FromBody] UserAuthRequest request)
        {
            return (await authHandler.IsAdmin(request.UserToken)) is { } result ? Ok(result) : BadRequest();
        }
        [HttpPost("AuthUser")]
        public async Task<IActionResult> AuthUser([FromBody] UserAuthRequest request)
        {
            return (await authHandler.AuthUser(request.UserToken)) is { } result ? Ok(result) : BadRequest();
        }
        [HttpPost("GetUserById")]
        public async Task<IActionResult> GetUserById([FromBody] UserRequest userName,CancellationToken cancellationToken)
        {
            return await(userHandler.GetUserById(userName.UserName,cancellationToken)) is { } result ? Ok(result) : BadRequest();
        }
        [HttpPost("ResetUser")]
        public async Task<IActionResult> ResetUser([FromBody]UserAuthRequest userRequest,CancellationToken cancellationToken)
        {
            return await(userHandler.ResetUser(userRequest.UserToken,cancellationToken)) is { } result ? Ok(result) : BadRequest();
        }
        
    }
}
