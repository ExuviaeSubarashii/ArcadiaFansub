using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.CommentRequest;
using ArcadiaFansub.Services.Services.EpisodeServices;
using ArcadiaFansub.Services.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.CommentServices
{
    public class CommentHandler(ArcadiaFansubContext AF,UserAuthentication auth) : ICommentInterface
    {
        public async Task<string> CreateEpisodeComment(CreateEpisodeCommentBody body, CancellationToken cancellationToken)
        {
            Comment newComment = new()
            {
                CommentContent = body.CommentContent,
                CommentDate = DateTime.Now,
                EpisodeId = body.EpisodeId,
                UserId = body.UserId,
                UserName = body.UserName,
            };
            await AF.Comments.AddAsync(newComment);
            await AF.SaveChangesAsync(cancellationToken);
            return "Created Comment";
        }

        public async Task<string> DeleteEpisodeComment(DeleteEpisodeCommentBody body, CancellationToken cancellationToken)
        {
            var commentQuery = await AF.Comments.FirstOrDefaultAsync(x => x.CommentId == body.CommentId);
            var userQuery=await AF.Users.FirstOrDefaultAsync(x => x.UserToken == body.UserToken);
            if (commentQuery != null&&IsCommentOwner.IsOwner(body.UserToken,userQuery.UserId)||await auth.IsAdmin(body.UserToken.Trim())==true)
            {
                AF.Comments.Remove(commentQuery);
                await AF.SaveChangesAsync(cancellationToken);
                return "Succesfully Deleted Comment";
            }
            else
            {
                return "Could not delete comment";
            }
        }

        public async Task<IEnumerable<CommentsDTO>> GetEpisodeComments(string episodeId, string userToken, CancellationToken cancellationToken)
        {

            var commentsQuery = await AF.Comments.Where(x => x.EpisodeId == episodeId).Select(x => new CommentsDTO
            {
                CommentId = x.CommentId,
                CommentContent = x.CommentContent,
                CommentDate = x.CommentDate,
                CommentTextDate = EpisodeHandler.GetDate(x.CommentDate),
                EpisodeId = episodeId,
                IsCommentOwner = IsCommentOwner.IsOwner(userToken, x.UserId),
                UserId = x.UserId,
                UserName = x.UserName
            }).ToListAsync(cancellationToken);
            return commentsQuery;
        }

        public async Task<string> UpdateEpisodeComment(UpdateEpisodeCommentBody body, CancellationToken cancellationToken)
        {
            var commentQuery = await AF.Comments.FirstOrDefaultAsync(x => x.CommentId == body.CommentId, cancellationToken);

            if (commentQuery != null&&!string.IsNullOrEmpty(body.NewComment))
            {
                if (IsCommentOwner.IsOwner(body.UserToken, commentQuery.UserId) == true)
                {
                    commentQuery.CommentContent = body.NewComment;
                    await AF.SaveChangesAsync(cancellationToken);
                    return "Succesfully updated";
                }
                else
                {
                    return "Could not update";
                }
            }
            else
            {
                return "Could not update";
            }
        }
    }
}
