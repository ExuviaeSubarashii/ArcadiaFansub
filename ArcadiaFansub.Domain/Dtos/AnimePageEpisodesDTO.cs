namespace ArcadiaFansub.Domain.Dtos
{
    public class AnimePageEpisodesDto
    {
        public required string EpisodeId { get; set; }
        public required string AnimeName { get; set; }
        public required int EpisodeNumber { get; set; }
    }
}
