using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.MemberRequest;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface IMemberInterface
    {
        Task<IEnumerable<MembersDTO>> GetAllMembers(CancellationToken cancellationToken);
        Task<string> RemoveOrAddMemberRole(RemoveMemberRoleRequest rm, CancellationToken cancellationToken);
        Task<string> CreateNewMember(CreateNewMemberRequest cr, CancellationToken cancellationToken);
    }
}
