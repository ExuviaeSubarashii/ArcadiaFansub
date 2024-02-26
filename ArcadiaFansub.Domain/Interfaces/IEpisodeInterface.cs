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
        Task<IEnumerable<EpisodesDTO>> GetAllEpisodes(CancellationToken cancellationToken);
        Task<string> AddNewEpisode(AddNewEpisodeRequest newEpisode, CancellationToken cancellationToken);
        Task<string> DeleteEpisode(DeleteEpisodeRequest deleteEpisode, CancellationToken cancellationToken);
        Task<string> UpdateEpisode(UpdateEpisodeRequest updateEpisode, CancellationToken cancellationToken);
        Task<EpisodePageDTO> GetThatEpisode(string episodeId,CancellationToken cancellationToken );
        Task<IEnumerable<EpisodesDTO>> GetEpisodePanelAnimeEpisodes(string animeId, CancellationToken cancellationToken);
        Task<IEnumerable<EpisodesDTO>> GetEpisodesByPageQuery(int offSet, CancellationToken cancellationToken);
        Task DeleteAllEpisodes(string animeId, CancellationToken cancellationToken);
        Task<int> GetAmountOfPages(CancellationToken cancellationToken);
    }
}
