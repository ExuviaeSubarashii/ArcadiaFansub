namespace ArcadiaFansub.Domain.Dtos
{
    public class TicketReplyDto
    {
        public int ResponseId { get; set; }
        public string TicketId { get; set; } = null!;
        public string TicketAdminName { get; set; } = null!;
        public string TicketReply { get; set; } = null!;
        public DateTime TicketReplyDate { get; set; }
    }
}
