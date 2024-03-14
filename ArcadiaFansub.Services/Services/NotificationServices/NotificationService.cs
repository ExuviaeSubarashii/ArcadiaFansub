using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.NotificationServices
{
    public class NotificationService(ArcadiaFansubContext AF) : INotificationService
    {
        public async Task<IEnumerable<EpisodeNotificationDto>> GetNotifications(string userToken)
        {
            List<string> userFavoritedSeries = new();
            List<EpisodeNotificationDto> returnedData = new();

            var userSeriesQuery = await AF.Users
                .Where(token => token.UserToken == userToken.Trim())
                .FirstOrDefaultAsync();
            if (userSeriesQuery != null)
            {

                userFavoritedSeries = userSeriesQuery.FavoritedAnimes.Split(",").ToList();

                returnedData = await AF.Episodes
                .Take(5)
                .Where(x => userFavoritedSeries.Contains(x.AnimeId))
                .Select(x => new EpisodeNotificationDto
                {
                    EpisodeLink = x.EpisodeId.Trim(),
                    EpisodeNotificationMessage = $"{x.AnimeName.Trim()} {x.EpisodeNumber}. Bölüm."
                })
                .ToListAsync();

                return returnedData;
            }
            else
            {
                return new List<EpisodeNotificationDto>();
            }
        }
    }
}
