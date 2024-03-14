namespace ArcadiaFansub.Domain.Dtos
{
    public class UserDto
    {
        public required int UserId { get; set; }
        public required string UserName { get; set; }
        public string? FavoritedAnimes { get; set; }
        public required string UserToken { get; set; }
        public required string UserPermission { get; set; }
        public required string UserEmail { get; set; }
    }
}
