using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.UserServices
{
    public class UserAuthentication(ArcadiaFansubContext AF) : IUserAuthInterface
    {
        public async Task<bool> IsAdmin(string userToken)
        {
            var adminCheck = await AF.Users
            .Where(x=> x.UserToken == userToken.Trim() && x.UserPermission == "admin")
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
            .Where(x=> x.UserToken == userToken.Trim())
            .AnyAsync();
            if (userCheck)
            {
                return true;
            }
            return false;
        }
    }
}
