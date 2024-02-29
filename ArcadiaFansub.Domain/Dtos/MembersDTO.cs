using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Dtos
{
    public class MembersDTO
    {
        public required string MemberName { get; set; }
        public required string MemberRole { get; set; }
    }
}
