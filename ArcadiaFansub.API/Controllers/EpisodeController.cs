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
        public async Task<ActionResult> AddNewEpisode([FromBody]AddNewEpisodeRequest newEpisode) 
        {
            return Ok(await episodeHandler.AddNewEpisode(newEpisode));
        }
        [HttpPost("DeleteEpisode")]
        public async Task<ActionResult> DeleteEpisodia([FromBody] DeleteEpisodeRequest deleteEpisode) 
        {
            return Ok(await episodeHandler.DeleteEpisode(deleteEpisode));
        }
        [HttpPut("UpdateEpisode")]
        public async Task<ActionResult> UpdateEpisode([FromBody] UpdateEpisodeRequest updateEpisode) 
        {
            return Ok(await episodeHandler.UpdateEpisode(updateEpisode));
        }
        [HttpGet("GetAllEpisodes")]
        public async Task<ActionResult> GetEpisodes() 
        {
            return Ok(await episodeHandler.GetAllEpisodes());
        }
        [HttpPost("GetVideo")]
        public async Task<ActionResult> GetVideo([FromBody] string episodeId)
        {
            return Ok(await episodeHandler.GetThatEpisode(episodeId));
        }
        [HttpPost("GetEpisodePanelData")]
        public async Task<ActionResult> GetEpisodePanelData([FromBody] GetThoseAnimes animeIdDto)
        {
            return Ok(await episodeHandler.GetEpisodePanelAnimeEpisodes(animeIdDto.AnimeId));
        }
        [HttpPost("GetEpisodesByPageNumber")]
        public async Task<ActionResult> GetEpisodesByPageNumber([FromBody]PageRequest offSetbody)
        {
            return Ok(await episodeHandler.GetEpisodesByPageQuery(offSetbody.OffSet));
        }
    }
}
