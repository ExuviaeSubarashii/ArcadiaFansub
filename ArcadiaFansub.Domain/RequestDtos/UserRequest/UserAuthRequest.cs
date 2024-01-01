using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.UserRequest
{
    public class UserAuthRequest
    {
        public required string UserToken { get; set; }
    }
}
