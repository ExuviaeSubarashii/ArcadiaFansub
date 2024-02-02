using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.CommentRequest;
using ArcadiaFansub.Services.Services.EpisodeServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.CommentServices
{
    public class CommentHandler(ArcadiaFansubContext AF) : ICommentInterface
    {
        public async Task<string> CreateEpisodeComment(CreateEpisodeCommentBody body)
        {
            Comment newComment = new()
            {
                CommentContent = body.CommentContent,
                CommentDate = DateTime.Now,
                EpisodeId = body.EpisodeId,
                UserId = body.UserId,
                UserName = body.UserName,
            };
            AF.Comments.Add(newComment);
            AF.SaveChanges();

            return "Created Comment";
        }

        public async Task<string> DeleteEpisodeComment(DeleteEpisodeCommentBody body)
        {
            var commentQuery = await AF.Comments.FirstOrDefaultAsync(x => x.CommentId == body.CommentId);
            if (commentQuery != null)
            {
                AF.Comments.Remove(commentQuery);
                AF.SaveChanges();
                return "Succesfully Deleted Comment";
            }
            else
            {
                return "Could not delete comment";
            }
        }

        public async Task<IEnumerable<CommentsDTO>> GetEpisodeComments(string episodeId, string userToken)
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
            }).ToListAsync();
            return commentsQuery;
        }

        public async Task<string> UpdateEpisodeComment(UpdateEpisodeCommentBody body)
        {
            var commentQuery = await AF.Comments.FirstOrDefaultAsync(x => x.CommentId == body.CommentId);

            if (commentQuery != null&&!string.IsNullOrEmpty(body.NewComment))
            {
                if (IsCommentOwner.IsOwner(body.UserToken, commentQuery.UserId) == true)
                {
                    commentQuery.CommentContent = body.NewComment;
                    AF.SaveChanges();
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
