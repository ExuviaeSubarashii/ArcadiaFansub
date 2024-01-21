using ArcadiaFansub.Domain.RequestDtos.AnimeRequest;
using ArcadiaFansub.Domain.RequestDtos.EpisodeRequest;
using ArcadiaFansub.Services.Services.EpisodeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController(EpisodeHandler episodeHandler) : ControllerBase
    {
        [HttpPost("AddNewEpisode")]
        public async Task<IActionResult> AddNewEpisode([FromBody]AddNewEpisodeRequest newEpisode) 
        {
            return Ok(await episodeHandler.AddNewEpisode(newEpisode));
        }
        [HttpPost("DeleteEpisode")]
        public async Task<IActionResult> DeleteEpisodia([FromBody] DeleteEpisodeRequest deleteEpisode) 
        {
            return Ok(await episodeHandler.DeleteEpisode(deleteEpisode));
        }
        [HttpPut("UpdateEpisode")]
        public async Task<IActionResult> UpdateEpisode([FromBody] UpdateEpisodeRequest updateEpisode) 
        {
            return Ok(await episodeHandler.UpdateEpisode(updateEpisode));
        }
        [HttpGet("GetAllEpisodes")]
        public async Task<IActionResult> GetEpisodes() 
        {
            return Ok(await episodeHandler.GetAllEpisodes());
        }
        [HttpPost("GetVideo")]
        public async Task<IActionResult> GetVideo([FromBody] string episodeId)
        {
            return Ok(await episodeHandler.GetThatEpisode(episodeId));
        }
        [HttpPost("GetEpisodePanelData")]
        public async Task<IActionResult> GetEpisodePanelData([FromBody] GetThoseAnimes animeIdDto)
        {
            return Ok(await episodeHandler.GetEpisodePanelAnimeEpisodes(animeIdDto.AnimeId));
        }
        [HttpPost("GetEpisodesByPageNumber")]
        public async Task<IActionResult> GetEpisodesByPageNumber([FromBody]PageRequest offSetbody)
        {
            return Ok(await episodeHandler.GetEpisodesByPageQuery(offSetbody.OffSet));
        }
    }
}
