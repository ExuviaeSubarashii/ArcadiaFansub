using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.AnimeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface IAnimeInterface
    {
        Task<IEnumerable<AnimesDTO>> GetAllAnimes(string userToken, CancellationToken cancellationToken);
        Task<IEnumerable<AnimesDTO>> GetAllAnimesBySearch(string searchInput, CancellationToken cancellationToken);
        Task<IEnumerable<AnimesDTO>> GetAllAnimesByAlphabet(string letter, CancellationToken cancellationToken);
        Task<string> DeleteAnime(string animeId, CancellationToken cancellationToken);
        Task<string> CreateAnime(AddNewAnimeRequest ar, CancellationToken cancellationToken);
        Task<AnimePageDTO> GetThatAnime(string animeId, CancellationToken cancellationToken);
        Task<IEnumerable<AnimePageEpisodesDTO>> GetThatAnimeEpisodeLinks(string animeId, CancellationToken cancellationToken);
        Task<int> GetEpisodeNumber(string animeId, CancellationToken cancellationToken);
        Task<IEnumerable<AnimesDTO>> GetUserFavoritedAnimes(string[] animeId, string userToken, CancellationToken cancellationToken);
        Task<string> AddOrRemoveAnimeToFavorites(string animeId, string userToken, CancellationToken cancellationToken);

        Task<string> UpdateAnimeProperties(UpdateAnimeRequest updateAnimeRequest, CancellationToken cancellationToken);
        Task<string> DeleteAnimeFromEverybody(string animeId, CancellationToken cancellationToken);

    }
}
