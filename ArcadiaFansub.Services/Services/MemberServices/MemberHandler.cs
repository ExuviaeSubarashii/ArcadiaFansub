using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.MemberRequest;
using ArcadiaFansub.Services.Services.UserServices;
using Microsoft.EntityFrameworkCore;

namespace ArcadiaFansub.Services.Services.MemberServices
{
    public class MemberHandler(ArcadiaFansubContext AF, UserAuthentication UA) : IMemberInterface
    {
        public async Task<string> CreateNewMember(CreateNewMemberRequest cr, CancellationToken cancellationToken)
        {
            var memberQuery = await AF.Members.FirstOrDefaultAsync(x => x.MemberName == cr.MemberName.Trim(), cancellationToken);
            if (memberQuery == null)
            {
                string roles = string.Join(",", cr.MemberRoles);
                Member newMember = new()
                {
                    MemberName = cr.MemberName,
                    MemberRole = roles
                };
                await AF.Members.AddAsync(newMember);
                await AF.SaveChangesAsync(cancellationToken);
                return $"Created New Member {cr.MemberName}.";
            }
            else
            {
                return "Could Not Create New Member.";
            }

        }

        public async Task<IEnumerable<MembersDTO>> GetAllMembers(CancellationToken cancellationToken)
        {
            var memberQuery = await AF.Members
                .Select(x => new MembersDTO
                {
                    MemberId = x.MemberId,
                    MemberName = x.MemberName.Trim(),
                    MemberRole = x.MemberRole.Trim()
                }).ToListAsync();
            if (memberQuery.Any()) { return memberQuery; }
            else { return new List<MembersDTO>(); }
        }

        public async Task<string> RemoveOrAddMemberRole(RemoveMemberRoleRequest rm, CancellationToken cancellationToken)
        {
            if (await UA.IsAdmin(rm.UserToken))
            {
                var memberQuery = await AF.Members.FirstOrDefaultAsync(x => x.MemberId == rm.UserId, cancellationToken);
                if (memberQuery != null)
                {
                    List<string> roleList = memberQuery.MemberRole.Trim().Split(',').ToList();
                    roleList.RemoveAll(x => x == "");
                    if (roleList.Contains(rm.RoleName.Trim()))
                    {
                        roleList.Remove(rm.RoleName.Trim());
                        memberQuery.MemberRole = string.Join(",", roleList);
                        await AF.SaveChangesAsync();
                        return $"Succesfully Removed {rm.RoleName} from {memberQuery.MemberName}";
                    }
                    roleList.Add(rm.RoleName.Trim());
                    memberQuery.MemberRole = string.Join(",", roleList);
                    await AF.SaveChangesAsync();
                    return $"Succesfully Added role {rm.RoleName} to{memberQuery.MemberName}";
                }
                else
                {
                    return "Could not adjust roles";
                }
            }
            else
            {
                return "Could not adjust roles";
            }
        }

    }
}
