using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.CommentRequest;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface ICommentInterface
    {
        Task<IEnumerable<CommentsDto>> GetEpisodeComments(string episodeId, string userToken, CancellationToken cancellationToken);
        Task<string> CreateEpisodeComment(CreateEpisodeCommentBody body, CancellationToken cancellationToken);
        Task<string> DeleteEpisodeComment(DeleteEpisodeCommentBody body, CancellationToken cancellationToken);
        Task<string> UpdateEpisodeComment(UpdateEpisodeCommentBody body, CancellationToken cancellationToken);
        Task<IEnumerable<CommentsDto>> GetUserComments(string userName, string viewerToken, CancellationToken cancellationToken);
        Task<string> DeleteAllComments(string episodeId, CancellationToken cancellationToken);
    }
}
