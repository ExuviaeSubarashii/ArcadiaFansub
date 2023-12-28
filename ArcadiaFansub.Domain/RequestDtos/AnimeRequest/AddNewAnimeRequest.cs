using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.AnimeRequest
{
    public class AddNewAnimeRequest
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
