using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.CommentRequest
{
    public class DeleteEpisodeCommentBody
    {
        public required string CommentId { get; set; }
    }
}
