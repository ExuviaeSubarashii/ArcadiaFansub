using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Dtos
{
    public class EpisodePageDTO
    {
        public string? AnimeName { get; set; }
        public int? EpisodeNumber { get; set; }
        public string? EpisodeLinks { get; set; }
        public string? EpisodeId {  get; set; }
        public string? AnimeId { get; set; }
    }
}
