using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArcadiaFansub.Domain.Dtos
{
    public class CommentsDTO
    {
        public required int CommentId { get; set; }
        public required string EpisodeId { get; set; }
        public required int UserId { get; set; }
        public required string UserName { get; set; }
        public required string CommentContent { get; set; }
        public required DateTime CommentDate { get; set; }
        public required string CommentTextDate { get; set; }
        public required bool IsCommentOwner { get; set; }
    }
}
