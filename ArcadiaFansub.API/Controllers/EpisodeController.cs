using ArcadiaFansub.Domain.RequestDtos.AnimeRequest;
using ArcadiaFansub.Domain.RequestDtos.EpisodeRequest;
using ArcadiaFansub.Services.Services.EpisodeServices;
using ArcadiaFansub.Services.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController(EpisodeHandler episodeHandler, UserAuthentication UA) : ControllerBase
    {
        [HttpPost("AddNewEpisode")]
        public async Task<IActionResult> AddNewEpisode([FromBody] AddNewEpisodeRequest newEpisode, CancellationToken cancellationToken)
        {
            return Ok(await episodeHandler.AddNewEpisode(newEpisode, cancellationToken));
        }
        [HttpDelete("DeleteEpisode")]
        public async Task<IActionResult> DeleteEpisodia([FromBody] DeleteEpisodeRequest deleteEpisode, CancellationToken cancellationToken)
        {
            return Ok(await episodeHandler.DeleteEpisode(deleteEpisode, cancellationToken));
        }
        [HttpPut("UpdateEpisode")]
        public async Task<IActionResult> UpdateEpisode([FromBody] UpdateEpisodeRequest updateEpisode, CancellationToken cancellationToken)
        {
            return Ok(await episodeHandler.UpdateEpisode(updateEpisode, cancellationToken));
        }
        [HttpGet("GetAllEpisodes")]
        public async Task<IActionResult> GetEpisodes(CancellationToken cancellationToken)
        {
            return Ok(await episodeHandler.GetAllEpisodes(cancellationToken));
        }
        [HttpPost("GetVideo")]
        public async Task<IActionResult> GetVideo([FromBody] string episodeId, CancellationToken cancellationToken)
        {
            return Ok(await episodeHandler.GetThatEpisode(episodeId, cancellationToken));
        }
        [HttpPost("GetEpisodePanelData")]
        public async Task<IActionResult> GetEpisodePanelData([FromBody] GetThoseAnimes animeIdDto, CancellationToken cancellationToken)
        {
            return Ok(await episodeHandler.GetEpisodePanelAnimeEpisodes(animeIdDto.AnimeId, cancellationToken));
        }
        [HttpPost("GetEpisodesByPageNumber")]
        [EnableRateLimiting("fixed")]

        public async Task<IActionResult> GetEpisodesByPageNumber([FromBody] PageRequest offSetbody, CancellationToken cancellationToken)
        {
            return await (episodeHandler.GetEpisodesByPageQuery(offSetbody.OffSet, cancellationToken)) is { } result ? Ok(result) : BadRequest();
        }
        [HttpGet("GetPageAmount")]
        public async Task<IActionResult> GetPageAmount(CancellationToken cancellationToken)
        {
            return Ok(await episodeHandler.GetAmountOfPages(cancellationToken));
        }
        [HttpPost("BulkDeleteEpisodes")]
        public async Task<IActionResult> BulkDeleteEpisodes([FromBody] BulkEpisodeDeleteRequest BED, CancellationToken cancellationToken)
        {
            if (await UA.IsAdmin(BED.userToken) == true)
            {
                return await (episodeHandler.BultDeleteImagesAsync(BED.episodeIds, BED.userToken, cancellationToken)) is { } result ? Ok(result) : BadRequest();
            }
            else
            {
                return BadRequest("User isn't an admin.");
            }

        }
    }
}
