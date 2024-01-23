using ArcadiaFansub.Domain.RequestDtos.UserRequest;
using ArcadiaFansub.Services.Services.NotificationServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(NotificationService NS) : ControllerBase
    {
        [HttpPost("GetNotifications")]
        public async Task<IActionResult> GetNotifications([FromBody] UserAuthRequest user)
        {
            if(!string.IsNullOrEmpty(user.UserToken))
            {
            return Ok(await NS.GetNotifications(user.UserToken));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
