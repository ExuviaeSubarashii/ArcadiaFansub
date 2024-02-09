using ArcadiaFansub.Domain.RequestDtos.CommentRequest;
using ArcadiaFansub.Domain.RequestDtos.UserRequest;
using ArcadiaFansub.Services.Services.CommentServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(CommentHandler CH) : ControllerBase
    {
        [HttpPost("CreateEpisodeComment")]
        public async Task<IActionResult> CreateEpisodeComment([FromBody] CreateEpisodeCommentBody body, CancellationToken cancellationToken)
        {
            return (await CH.CreateEpisodeComment(body, cancellationToken)) is { } result ? Ok(result) : BadRequest();
        }
        [HttpDelete("DeleteEpisodeComment")]
        public async Task<IActionResult> DeleteEpisodeComment([FromBody] DeleteEpisodeCommentBody body, CancellationToken cancellationToken)
        {
            return (await CH.DeleteEpisodeComment(body, cancellationToken)) is { } result ? Ok(result) : BadRequest();
        }
        [HttpPost("GetEpisodeComments/{episodeId}")]
        public async Task<IActionResult> GetEpisodeComments([FromBody] UserAuthRequest request, string episodeId, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(episodeId))
            {
                return Ok(await CH.GetEpisodeComments(episodeId, request.UserToken, cancellationToken));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("UpdateEpisodeComment")]
        public async Task<IActionResult> UpdateEpisodeComment([FromBody] UpdateEpisodeCommentBody body, CancellationToken cancellationToken)
        {
            return (await CH.UpdateEpisodeComment(body, cancellationToken)) is { } result ? Ok(result) : BadRequest();
        }

        [HttpPost("GetUserComments/{userName}")]
        public async Task<IActionResult> GetUserComments([FromBody] UserAuthRequest request,string userName, CancellationToken cancellationToken)
        {
            return (await CH.GetUserComments(userName,request.UserToken,cancellationToken)) is { } result ? Ok(result) : BadRequest();
        }
    }
}
