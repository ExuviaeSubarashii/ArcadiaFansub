using System;
using System.Collections.Generic;

namespace ArcadiaFansub.Domain.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserToken { get; set; } = null!;
        public string? FavoritedAnimes { get; set; }
        public string UserPermission {  get; set; } = null!;
    }
}
