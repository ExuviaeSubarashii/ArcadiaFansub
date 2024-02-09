using ArcadiaFansub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.AnimeServices
{
    public class IsFavorited()
    {
        public static bool IsFavoritedByUser(string userToken, string animeId)
        {
            using ArcadiaFansubContext AF = new ArcadiaFansubContext();
            var userQuery = AF.Users.FirstOrDefault(x => x.UserToken == userToken);
            if (userQuery != null)
            {
                List<string> favoritedSeries = userQuery.FavoritedAnimes.Split(',').ToList();
                bool isFavorited = favoritedSeries.Contains(animeId.Trim());
                return isFavorited;
            }
            else { return false; }
        }
    }
}
