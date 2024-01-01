using System;
using System.Collections.Generic;

namespace ArcadiaFansub.Domain.Models
{
    public partial class Episode
    {
        public string EpisodeId { get; set; } = null!;
        public string AnimeId { get; set; }
        public string AnimeName { get; set; } = null!;
        public int EpisodeNumber { get; set; }
        public string EpisodeLinks { get; set; } = null!;
        public int EpisodeLikes { get; set; }
        public DateTime EpisodeUploadDate { get; set; }

        //relationship
        public Anime? Anime { get; set; }

    }
}
