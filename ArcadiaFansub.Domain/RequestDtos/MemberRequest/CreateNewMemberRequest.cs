namespace ArcadiaFansub.Domain.RequestDtos.MemberRequest
{
    public class CreateNewMemberRequest
    {
        public string MemberName { get; set; }
        public string[] MemberRoles { get; set; }
    }
}
