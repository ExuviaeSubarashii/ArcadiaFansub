using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface IUserAuthInterface
    {
        Task<bool> IsAdmin(string userToken);
        Task<bool> AuthUser(string userToken);
    }
}
