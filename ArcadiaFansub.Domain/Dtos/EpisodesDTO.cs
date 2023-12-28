using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Dtos
{
    public class EpisodesDTO
    {
        public int EpisodeId { get; set; }
        public string AnimeName { get; set; } = null!;
        public int EpisodeNumber { get; set; }
        public string EpisodeLinks { get; set; } = null!;
        public int EpisodeLikes { get; set; }
        public DateTime EpisodeUploadDate { get; set; }
    }
}
