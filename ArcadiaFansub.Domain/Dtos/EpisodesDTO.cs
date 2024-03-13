namespace ArcadiaFansub.Domain.Dtos
{
    public class EpisodesDTO
    {
        public string EpisodeId { get; set; } = null!;
        public string AnimeName { get; set; } = null!;
        public int EpisodeNumber { get; set; }
        public string EpisodeLinks { get; set; } = null!;
        public DateTime EpisodeUploadDate { get; set; }
        public string AnimeImage { get; set; }
        public string AnimeId { get; set; } = null!;
        public string SortingDate { get; set; } = null!;
    }
}
