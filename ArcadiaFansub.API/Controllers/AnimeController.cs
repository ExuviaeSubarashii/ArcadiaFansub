using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.AnimeRequest;
using ArcadiaFansub.Services.Services.AnimeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController(AnimeHandler animeHandler) : ControllerBase
    {
        [HttpGet("GetAllAnimes")]
        public async Task<ActionResult> GetAllAnimes()
        {
            return Ok(await animeHandler.GetAllAnimes());
        }
        [HttpPost("GetAnimeByAlphabet")]
        public async Task<ActionResult> GetAnimeByAlphabet([FromBody] ByAlphabetRequest alphabetSearch)
        {
            return Ok(await animeHandler.GetAllAnimesByAlphabet(alphabetSearch.AlphabetValue));
        }
        [HttpPost("GetAnimeBySearch")]
        public async Task<ActionResult> GetAnimeBySearch([FromBody]AnimeByInputValueRequest inputValue)
        {
            return Ok(await animeHandler.GetAllAnimesBySearch(inputValue.InputValue));
        }
        [HttpPost("DeleteAnime")]
        public async Task<ActionResult> DeleteAnime([FromBody] AnimeDeleteRequest deleteRequest)
        {
            return Ok(await animeHandler.DeleteAnime(deleteRequest.animeId));
        }
        [HttpPost("CreateNewAnime")]
        public async Task<ActionResult> CreateNewAnime([FromBody] AddNewAnimeRequest animeRequest)
        {
            return Ok(await animeHandler.CreateAnime(animeRequest));
        }
        [HttpPost("GetAnimeProperties")]
        public async Task<ActionResult> GetAnimeProperties([FromBody] GetThoseAnimes anime)
        {
            return Ok(await animeHandler.GetThatAnime(anime.AnimeId));
        }
        [HttpPost("GetAnimeEpisodes")]
        public async Task<ActionResult> GetAnimeEpisodes([FromBody] GetThoseAnimes anime)
        {
            return Ok(await animeHandler.GetThatAnimeEpisodeLinks(anime.AnimeId));
        }
    }
}
