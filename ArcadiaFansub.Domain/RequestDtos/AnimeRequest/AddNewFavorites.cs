using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.AnimeRequest
{
    public class AddNewFavorites
    {
        public required string UserToken { get; set; }
        public required string AnimeId { get; set; }
    }
}
