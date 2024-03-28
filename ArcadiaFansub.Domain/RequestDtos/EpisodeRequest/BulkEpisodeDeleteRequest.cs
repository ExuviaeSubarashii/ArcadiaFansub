namespace ArcadiaFansub.Domain.RequestDtos.EpisodeRequest
{
    public record BulkEpisodeDeleteRequest(string[] episodeIds, string userToken);
}
