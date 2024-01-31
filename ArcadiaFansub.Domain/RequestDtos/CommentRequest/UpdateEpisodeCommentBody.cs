using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.CommentRequest
{
    public class UpdateEpisodeCommentBody
    {
        public required string CommentId { get; set; }
        public required string UserId { get; set; }
        public required string NewComment {  get; set; }
    }
}
