using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Dtos
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? FavoritedAnimes { get; set; }
        public required string UserToken { get; set; }
        public string UserPermission { get; set; } = null!;
    }
}
