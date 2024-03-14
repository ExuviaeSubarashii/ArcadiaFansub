using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.EpisodeRequest;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface IEpisodeInterface
    {
        Task<IEnumerable<EpisodesDto>> GetAllEpisodes(CancellationToken cancellationToken);
        Task<string> AddNewEpisode(AddNewEpisodeRequest newEpisode, CancellationToken cancellationToken);
        Task<string> DeleteEpisode(DeleteEpisodeRequest deleteEpisode, CancellationToken cancellationToken);
        Task<string> UpdateEpisode(UpdateEpisodeRequest updateEpisode, CancellationToken cancellationToken);
        Task<EpisodePageDto> GetThatEpisode(string episodeId, CancellationToken cancellationToken);
        Task<IEnumerable<EpisodesDto>> GetEpisodePanelAnimeEpisodes(string animeId, CancellationToken cancellationToken);
        Task<IEnumerable<EpisodesDto>> GetEpisodesByPageQuery(int offSet, CancellationToken cancellationToken);
        Task DeleteAllEpisodes(string animeId, CancellationToken cancellationToken);
        Task<int> GetAmountOfPages(CancellationToken cancellationToken);
    }
}
