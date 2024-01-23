using ArcadiaFansub.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<EpisodeNotificationDTO>> GetNotifications(string userToken);
    }
}
