using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.CommentRequest
{
    public class CreateEpisodeCommentBody
    {
        public required string EpisodeId { get; set; } 
        public required int UserId { get; set; } 
        public required string UserName { get; set; } 
        public required string CommentContent { get; set; } 
        public required string CommentTextDate { get; set; } 
    }
}
