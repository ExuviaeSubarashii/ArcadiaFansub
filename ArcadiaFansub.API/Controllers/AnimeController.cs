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
        public async Task<IActionResult> GetAllAnimes()
        {
            return Ok(await animeHandler.GetAllAnimes());
        }
        [HttpPost("GetAnimeByAlphabet")]
        public async Task<IActionResult> GetAnimeByAlphabet([FromBody] ByAlphabetRequest alphabetSearch)
        {
            return Ok(await animeHandler.GetAllAnimesByAlphabet(alphabetSearch.AlphabetValue));
        }
        [HttpPost("GetAnimeBySearch")]
        public async Task<IActionResult> GetAnimeBySearch([FromBody]AnimeByInputValueRequest inputValue)
        {
            return Ok(await animeHandler.GetAllAnimesBySearch(inputValue.InputValue));
        }
        [HttpPost("DeleteAnime")]
        public async Task<IActionResult> DeleteAnime([FromBody] AnimeDeleteRequest deleteRequest)
        {
            return Ok(await animeHandler.DeleteAnime(deleteRequest.animeId));
        }
        [HttpPost("CreateNewAnime")]
        public async Task<IActionResult> CreateNewAnime([FromBody] AddNewAnimeRequest animeRequest)
        {
            return Ok(await animeHandler.CreateAnime(animeRequest));
        }
        [HttpPost("GetAnimeProperties")]
        public async Task<IActionResult> GetAnimeProperties([FromBody] GetThoseAnimes anime)
        {
            return Ok(await animeHandler.GetThatAnime(anime.AnimeId));
        }
        [HttpPost("GetAddEpisodeNumber")]
        public async Task<IActionResult> GetEpisodeNumber([FromBody] GetThoseAnimes anime)
        {
            return Ok(await animeHandler.GetEpisodeNumber(anime.AnimeId));
        }
        [HttpPost("GetAnimeEpisodes")]
        public async Task<IActionResult> GetAnimeEpisodes([FromBody] GetThoseAnimes anime)
        {
            return Ok(await animeHandler.GetThatAnimeEpisodeLinks(anime.AnimeId));
        }
    }
}
