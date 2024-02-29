using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Models
{
    public partial class Member
    {
        public int MemberId { get; set; }
        public required string MemberName { get; set; }
        public required string MemberRole { get; set; }
    }
}
