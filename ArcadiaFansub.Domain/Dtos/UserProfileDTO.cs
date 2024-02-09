using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Dtos
{
    public class UserProfileDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? FavoritedAnimes { get; set; }
        public string UserPermission { get; set; } = null!;
        public required string UserEmail { get; set; }
    }
}
