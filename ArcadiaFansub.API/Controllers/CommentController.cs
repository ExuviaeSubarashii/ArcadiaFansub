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
        public async Task<IActionResult> CreateEpisodeComment([FromBody] CreateEpisodeCommentBody body)
        {
            if(body is CreateEpisodeCommentBody)
            {
                return Ok(await CH.CreateEpisodeComment(body));
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }
        [HttpDelete("DeleteEpisodeComment")]
        public async Task<IActionResult> DeleteEpisodeComment([FromBody] DeleteEpisodeCommentBody body)
        {
            if (body is DeleteEpisodeCommentBody)
            {
                return Ok(await CH.DeleteEpisodeComment(body));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("GetEpisodeComments/{episodeId}")]
        public async Task<IActionResult> GetEpisodeComments([FromBody]UserAuthRequest request,string episodeId)
        {
            if(!string.IsNullOrEmpty(episodeId))
            {
                return Ok(await CH.GetEpisodeComments(episodeId,request.UserToken));
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("UpdateEpisodeComment")]
        public async Task<IActionResult> UpdateEpisodeComment([FromBody] UpdateEpisodeCommentBody body)
        {
            if(body is UpdateEpisodeCommentBody)
            {
                return Ok(await CH.UpdateEpisodeComment(body));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
