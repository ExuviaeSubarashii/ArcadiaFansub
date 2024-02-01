using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.CommentRequest
{
    public class UpdateEpisodeCommentBody
    {
        public required int CommentId { get; set; }
        public required int UserId { get; set; }
        public required string NewComment {  get; set; }
    }
}
