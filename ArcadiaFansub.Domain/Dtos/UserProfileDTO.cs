namespace ArcadiaFansub.Domain.Dtos
{
    public class UserProfileDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? FavoritedAnimes { get; set; }
        public string UserPermission { get; set; } = null!;
        public required string UserEmail { get; set; }
    }
}
