using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.AnimeRequest;
using ArcadiaFansub.Domain.RequestDtos.UserRequest;
using ArcadiaFansub.Services.Services.AnimeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController(AnimeHandler animeHandler) : ControllerBase
    {
        [HttpPost("GetAllAnimes")]
        public async Task<ActionResult> GetAllAnimes([FromBody] UserAuthRequest request,CancellationToken cancellationToken)
        {
            return (await animeHandler.GetAllAnimes(request.UserToken,cancellationToken)) is { } result ? Ok(result) : NotFound();
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
        [HttpPost("GetSpecificAnime")]
        public async Task<IActionResult> GetSpecificAnime([FromBody] GetFavoritedRequest anime, CancellationToken cancellationToken)
        {
            return (await animeHandler.GetUserFavoritedAnimes(anime.FavoritedAnimes,anime.UserToken, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPost("AddAnimeToFavorites")]
        public async Task<IActionResult> AddAnimeToFavorites([FromBody] AddNewFavorites request,CancellationToken cancellationToken)
        {
            return (await animeHandler.AddOrRemoveAnimeToFavorites(request.AnimeId,request.UserToken, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPut("UpdateAnimeProperties")]
        public async Task<IActionResult> UpdateAnimeProperties([FromBody] UpdateAnimeRequest request, CancellationToken cancellationToken)
        {
            return (await animeHandler.UpdateAnimeProperties(request, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
    }
}
