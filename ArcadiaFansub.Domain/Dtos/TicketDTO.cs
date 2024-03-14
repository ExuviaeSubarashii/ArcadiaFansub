namespace ArcadiaFansub.Domain.Dtos
{
    public class TicketDto
    {
        public string TicketId { get; set; } = null!;
        public string TicketTitle { get; set; } = null!;
        public string TicketMessage { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public string TicketDate { get; set; } = null!;
        public string TicketReason { get; set; } = null!;
        public string TicketStatus { get; set; } = null!;
        public DateTime TicketDateCreated { get; set; }
    }
}
