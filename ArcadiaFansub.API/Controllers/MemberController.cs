using ArcadiaFansub.Services.Services.MemberServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController(MemberHandler MH) : ControllerBase
    {
        [HttpGet("GetAllMembers")]
        public async Task<IActionResult> GetAllMembers(CancellationToken cancellationToken)
        {
            return await(MH.GetAllMembers(cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
    }
}
