using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Dtos
{
    public class AnimePageEpisodesDTO
    {
        public required string EpisodeId { get; set; }
        public required string AnimeName { get; set; }
        public required int EpisodeNumber { get; set; }
    }
}
