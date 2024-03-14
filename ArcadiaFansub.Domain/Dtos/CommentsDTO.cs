namespace ArcadiaFansub.Domain.Dtos
{
    public class CommentsDto
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
