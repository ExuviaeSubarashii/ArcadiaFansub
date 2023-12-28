using System;
using System.Collections.Generic;

namespace ArcadiaFansub.Domain.Models
{
    public partial class Anime
    {
        public int AnimeId { get; set; }
        public string AnimeName { get; set; } = null!;
        public int AnimeEpisodeAmount { get; set; }
        public string Links { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string Translator { get; set; } = null!;
        public string Editor { get; set; } = null!;
    }
}
