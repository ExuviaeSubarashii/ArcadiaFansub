using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ArcadiaFansub.Services.Services.UserServices
{
    public class UserAuthentication(ArcadiaFansubContext AF) : IUserAuthInterface
    {
        public async Task<bool> IsAdmin(string userToken)
        {
            var adminCheck = await AF.Users
            .Where(x => x.UserToken == userToken.Trim() && x.UserPermission == "admin")
            .AnyAsync();
            if (adminCheck)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> AuthUser(string userToken)
        {
            var userCheck = await AF.Users
            .Where(x => x.UserToken == userToken.Trim())
            .AnyAsync();
            if (userCheck)
            {
                return true;
            }
            return false;
        }
        public async Task<UserDto> ResetUser(string userToken)
        {
            var userQuery = await AF.Users.Where(x => x.UserToken == userToken.Trim()).Select(x => new UserDto
            {
                UserEmail = x.UserEmail,
                UserToken = x.UserToken,
                UserId = x.UserId,
                UserName = x.UserName,
                UserPermission = x.UserPermission,
            }).FirstOrDefaultAsync();
            if (userQuery != null)
            {
                return userQuery;
            }
            else
            {
                return null;
            }
        }
    }
}
