﻿using ArcadiaFansub.Domain.RequestDtos.MemberRequest;
using ArcadiaFansub.Services.Services.MemberServices;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController(MemberHandler MH) : ControllerBase
    {
        [HttpGet("GetAllMembers")]
        public async Task<IActionResult> GetAllMembers(CancellationToken cancellationToken)
        {
            return await (MH.GetAllMembers(cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPost("AddOrRemoveRole")]
        public async Task<IActionResult> AddOrRemoveRole([FromBody] RemoveMemberRoleRequest rm, CancellationToken cancellationToken)
        {
            return await (MH.RemoveOrAddMemberRole(rm, cancellationToken)) is { } result ? Ok(result.ToString()) : BadRequest();
        }
        [HttpPost("CreateNewMember")]
        public async Task<IActionResult> CreateNewMember([FromBody] CreateNewMemberRequest cr, CancellationToken cancellationToken)
        {
            return await (MH.CreateNewMember(cr, cancellationToken)) is { } result ? Ok(result) : BadRequest();
        }
    }
}
