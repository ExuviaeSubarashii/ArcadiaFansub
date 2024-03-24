namespace ArcadiaFansub.Domain.Models
{
    public partial class TicketReply
    {
        public int ResponseId { get; set; }
        public string TicketId { get; set; } = null!;
        public string TicketReplierName { get; set; } = null!;
        public string TicketMessage { get; set; } = null!;
        public DateTime TicketReplyDate { get; set; }
    }
}
