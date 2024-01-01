using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Dtos
{
    public class AnimesDTO
    {
        public string AnimeId { get; set; }
        public string AnimeName { get; set; } = null!;
        public int AnimeEpisodeAmount { get; set; }
        public string ReleaseDate { get; set; }=null!;
        public string Translator { get; set; } = null!;
        public string Editor { get; set; } = null!;
        public string AnimeImage { get; set; } = null!;


    }
}
