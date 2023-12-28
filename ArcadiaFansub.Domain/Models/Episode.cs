using System;
using System.Collections.Generic;

namespace ArcadiaFansub.Domain.Models
{
    public partial class Episode
    {
        public int EpisodeId { get; set; }
        public string AnimeName { get; set; } = null!;
        public int EpisodeNumber { get; set; }
        public string EpisodeLinks { get; set; } = null!;
        public int EpisodeLikes { get; set; }
        public DateTime EpisodeUploadDate { get; set; }
    }
}
