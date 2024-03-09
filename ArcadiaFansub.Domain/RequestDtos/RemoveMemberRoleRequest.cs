namespace ArcadiaFansub.Domain.RequestDtos
{
    public class RemoveMemberRoleRequest
    {
        public required int UserId { get; set; }
        public required string RoleName { get; set; }
        public required string UserToken { get; set; }
    }
}
