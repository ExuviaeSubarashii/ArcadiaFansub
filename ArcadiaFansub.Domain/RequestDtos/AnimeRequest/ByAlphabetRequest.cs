using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.AnimeRequest
{
    public class ByAlphabetRequest
    {
        public required string AlphabetValue { get; set; }
    }
}
