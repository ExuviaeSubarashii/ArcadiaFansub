using ArcadiaFansub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.CommentServices
{
    public class IsCommentOwner
    {
        public static bool IsOwner(string userToken, int userId)
        {
            using ArcadiaFansubContext AF = new ArcadiaFansubContext();
            var userQuery = AF.Users.FirstOrDefault(x => x.UserToken == userToken);
            if(userQuery!=null)
            {
                bool isOwner = userQuery.UserId == userId?true:false;
                return isOwner;
            }
            else
            {
                return false;
            }
        }
    }
}
