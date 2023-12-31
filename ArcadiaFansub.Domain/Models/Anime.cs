using System;
using System.Collections.Generic;

namespace ArcadiaFansub.Domain.Models
{
    public partial class Anime
    {
        public string AnimeId { get; set; }
        public string AnimeName { get; set; } = null!;
        public int AnimeEpisodeAmount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Translator { get; set; } = null!;
        public string Editor { get; set; } = null!;
        public string AnimeImage {  get; set; } = null!;
        //relationship
        public List<Episode>? Episodes { get; set; }
    }
}
