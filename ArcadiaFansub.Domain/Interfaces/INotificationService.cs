using ArcadiaFansub.Domain.Dtos;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<EpisodeNotificationDto>> GetNotifications(string userToken);
    }
}
