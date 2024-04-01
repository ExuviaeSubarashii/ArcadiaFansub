namespace ArcadiaFansub.Domain.Dtos
{
    public class AnimePageDto
    {
        public string AnimeName { get; set; } = null!;
        public int AnimeEpisodeAmount { get; set; }
        public string ReleaseDate { get; set; } = null!;
        public string Translator { get; set; } = null!;
        public string Editor { get; set; } = null!;
        public string AnimeImage { get; set; } = null!;
        public string AnimeId { get; set; } = null!;
        public string AnimeDescription { get; set; } = null!;
    }
}
