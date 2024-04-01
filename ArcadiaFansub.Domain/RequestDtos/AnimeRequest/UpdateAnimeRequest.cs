namespace ArcadiaFansub.Domain.RequestDtos.AnimeRequest
{
    public class UpdateAnimeRequest
    {
        public required string AnimeId { get; set; }
        public string? NewAnimeName { get; set; } = null!;
        public int? NewEpisodeAmount { get; set; }
        public string? NewEditorName { get; set; } = null!;
        public string? NewTranslatorName { get; set; } = null!;
        public DateTime? NewReleaseDate { get; set; }
        public string? NewDescription { get; set; }
    }
}
