using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Dtos
{
    public class EpisodeNotificationDTO
    {
        public required string EpisodeLink { get; set; }
        public required string EpisodeNotificationMessage { get; set; }
    }
}
