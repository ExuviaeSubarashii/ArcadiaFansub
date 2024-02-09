using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.AnimeRequest
{
    public class GetFavoritedRequest
    {
        public string[]? FavoritedAnimes { get; set; }
    }
}
