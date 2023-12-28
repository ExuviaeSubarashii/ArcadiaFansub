using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.UserRequest
{
    public class UserLoginRequest
    {
        public required string UserEmail {  get; set; }
        public required string Password { get; set; }
    }
}
