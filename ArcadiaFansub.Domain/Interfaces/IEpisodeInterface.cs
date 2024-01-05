using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.EpisodeRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface IEpisodeInterface
    {
        Task<IEnumerable<EpisodesDTO>> GetAllEpisodes();
        Task<string> AddNewEpisode(AddNewEpisodeRequest newEpisode);
        Task<string> DeleteEpisode(DeleteEpisodeRequest deleteEpisode);
        Task<string> UpdateEpisode(UpdateEpisodeRequest updateEpisode);
        Task<EpisodePageDTO> GetThatEpisode(string episodeId);
        Task<IEnumerable<EpisodesDTO>> GetEpisodePanelAnimeEpisodes(string animeId);
        Task<IEnumerable<EpisodesDTO>> GetEpisodesByPageQuery(int offSet);

    }
}
