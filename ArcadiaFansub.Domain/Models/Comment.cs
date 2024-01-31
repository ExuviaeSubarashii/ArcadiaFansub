using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArcadiaFansub.Domain.Models
{
    public partial class Comment
    {
        public string CommentId { get; set; }=null!;
        public string EpisodeId { get; set; }=null!;
        public int UserId { get; set; }
        public string UserName { get; set; }=null!;
        public string CommentContent { get; set; }=null!;
        public DateTime CommentDate { get; set; }
    }
}
