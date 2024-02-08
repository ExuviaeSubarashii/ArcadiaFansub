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
        public async Task<IActionResult> GetAllAnimes(CancellationToken cancellationToken)
        {
            return (await animeHandler.GetAllAnimes(cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPost("GetAnimeByAlphabet")]
        public async Task<IActionResult> GetAnimeByAlphabet([FromBody] ByAlphabetRequest alphabetSearch, CancellationToken cancellationToken)
        {
            return (await animeHandler.GetAllAnimesByAlphabet(alphabetSearch.AlphabetValue, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPost("GetAnimeBySearch")]
        public async Task<IActionResult> GetAnimeBySearch([FromBody] AnimeByInputValueRequest inputValue, CancellationToken cancellationToken)
        {
            return (await animeHandler.GetAllAnimesBySearch(inputValue.InputValue, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPost("DeleteAnime")]
        public async Task<IActionResult> DeleteAnime([FromBody] AnimeDeleteRequest deleteRequest, CancellationToken cancellationToken)
        {
            return (await animeHandler.DeleteAnime(deleteRequest.animeId, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPost("CreateNewAnime")]
        public async Task<IActionResult> CreateNewAnime([FromBody] AddNewAnimeRequest animeRequest, CancellationToken cancellationToken)
        {
            return (await animeHandler.CreateAnime(animeRequest, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPost("GetAnimeProperties")]
        public async Task<IActionResult> GetAnimeProperties([FromBody] GetThoseAnimes anime, CancellationToken cancellationToken)
        {
            return (await animeHandler.GetThatAnime(anime.AnimeId, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPost("GetAddEpisodeNumber")]
        public async Task<IActionResult> GetEpisodeNumber([FromBody] GetThoseAnimes anime, CancellationToken cancellationToken)
        {
            return (await animeHandler.GetEpisodeNumber(anime.AnimeId, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPost("GetAnimeEpisodes")]
        public async Task<IActionResult> GetAnimeEpisodes([FromBody] GetThoseAnimes anime, CancellationToken cancellationToken)
        {
            return (await animeHandler.GetThatAnimeEpisodeLinks(anime.AnimeId, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
    }
}
