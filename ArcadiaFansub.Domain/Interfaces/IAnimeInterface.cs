using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.AnimeRequest;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface IAnimeInterface
    {
        Task<IEnumerable<AnimesDto>> GetAllAnimes(string userToken, CancellationToken cancellationToken);
        Task<IEnumerable<AnimesDto>> GetAllAnimesBySearch(string searchInput, CancellationToken cancellationToken);
        Task<IEnumerable<AnimesDto>> GetAllAnimesByAlphabet(string letter, CancellationToken cancellationToken);
        Task<string> DeleteAnime(string animeId, CancellationToken cancellationToken);
        Task<string> CreateAnime(AddNewAnimeRequest ar, CancellationToken cancellationToken);
        Task<AnimePageDto> GetThatAnime(string animeId, CancellationToken cancellationToken);
        Task<IEnumerable<AnimePageEpisodesDto>> GetThatAnimeEpisodeLinks(string animeId, CancellationToken cancellationToken);
        Task<int> GetEpisodeNumber(string animeId, CancellationToken cancellationToken);
        Task<IEnumerable<AnimesDto>> GetUserFavoritedAnimes(string[] animeId, string userToken, CancellationToken cancellationToken);
        Task<string> AddOrRemoveAnimeToFavorites(string animeId, string userToken, CancellationToken cancellationToken);

        Task<string> UpdateAnimeProperties(UpdateAnimeRequest updateAnimeRequest, CancellationToken cancellationToken);
        Task<string> DeleteAnimeFromEverybody(string animeId, CancellationToken cancellationToken);

    }
}
