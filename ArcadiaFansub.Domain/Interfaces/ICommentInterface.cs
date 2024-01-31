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
        Task<IEnumerable<CommentsDTO>> GetEpisodeComments(string episodeId,string userToken);
        Task<string> CreateEpisodeComment(CreateEpisodeCommentBody body);
        Task<string> DeleteEpisodeComment(DeleteEpisodeCommentBody body);
        Task<string> UpdateEpisodeComment(UpdateEpisodeCommentBody body);
    }
}
