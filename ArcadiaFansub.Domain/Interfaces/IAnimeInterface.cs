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
        Task<IEnumerable<AnimesDTO>> GetAllAnimes();
        Task<IEnumerable<AnimesDTO>> GetAllAnimesBySearch(string searchInput);
        Task<IEnumerable<AnimesDTO>> GetAllAnimesByAlphabet(string letter);
        Task<string> DeleteAnime(string animeId);
        Task<string> CreateAnime(AddNewAnimeRequest ar);
        Task<AnimePageDTO> GetThatAnime(string animeId);
        Task<IEnumerable<AnimePageEpisodesDTO>> GetThatAnimeEpisodeLinks(string animeId);
        Task<int> GetEpisodeNumber(string animeId);
    }
}
