using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.TicketRequest
{
    public class TicketBody
    {
        public required string TicketTitle { get; set; }
        public required string TicketMessage { get; set; }
        public required string SenderName { get; set; }
        public required string TicketReason { get; set; }
        public required string TicketStatus { get; set; }
    }
}
