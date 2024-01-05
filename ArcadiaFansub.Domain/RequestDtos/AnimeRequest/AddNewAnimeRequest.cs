using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.AnimeRequest
{
    public class AddNewAnimeRequest
    {
        public string AnimeName { get; set; } = null!;
        public int AnimeEpisodeAmount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Translator { get; set; } = null!;
        public string Editor { get; set; } = null!;
        public string ImageLink {  get; set; }
    }
}
