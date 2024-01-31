using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Models
{
    public partial class UserTicket
    {
        public string TicketId { get; set; } = null!;
        public string TicketTitle { get; set; } = null!;
        public string TicketMessage { get; set; } = null!;
        public string SenderName { get; set; } = null!;
        public DateTime TicketDate { get; set; }
        public string TicketReason { get; set; } = null!;
        public string TicketStatus { get; set; } = null!;
        public string SenderToken { get; set; } = null!;
    }
}
