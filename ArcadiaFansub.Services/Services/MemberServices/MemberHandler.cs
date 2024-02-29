using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.MemberServices
{
    public class MemberHandler(ArcadiaFansubContext AF) : IMemberInterface
    {
        public async Task<IEnumerable<MembersDTO>> GetAllMembers(CancellationToken cancellationToken)
        {
            var memberQuery = await AF.Members.Select(x => new MembersDTO
            {
                MemberName = x.MemberName,
                MemberRole = x.MemberRole
            }).ToListAsync();
            if (memberQuery.Any()) { return memberQuery; }
            else { return new List<MembersDTO>(); }
        }
    }
}
