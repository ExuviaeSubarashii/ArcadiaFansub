using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.EpisodeRequest
{
    public class UpdateEpisodeRequest
    {
        public string AnimeName { get; set; } = null!;
        public string EpisodeId { get; set; }
        public string EpisodeLinks { get; set; } = null!;
    }
}
