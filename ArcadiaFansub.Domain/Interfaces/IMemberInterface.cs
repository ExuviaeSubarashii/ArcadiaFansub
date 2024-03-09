using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface IMemberInterface
    {
        Task<IEnumerable<MembersDTO>> GetAllMembers(CancellationToken cancellationToken);
        Task<string> RemoveOrAddMemberRole(RemoveMemberRoleRequest rm, CancellationToken cancellationToken);
    }
}
