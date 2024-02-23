using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.CommentRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface ICommentInterface
    {
        Task<IEnumerable<CommentsDTO>> GetEpisodeComments(string episodeId,string userToken, CancellationToken cancellationToken);
        Task<string> CreateEpisodeComment(CreateEpisodeCommentBody body, CancellationToken cancellationToken);
        Task<string> DeleteEpisodeComment(DeleteEpisodeCommentBody body, CancellationToken cancellationToken);
        Task<string> UpdateEpisodeComment(UpdateEpisodeCommentBody body, CancellationToken cancellationToken);
        Task<IEnumerable<CommentsDTO>> GetUserComments(string userName,string viewerToken,CancellationToken cancellationToken);
        Task<string> DeleteAllComments(string episodeId,CancellationToken cancellationToken);
    }
}
