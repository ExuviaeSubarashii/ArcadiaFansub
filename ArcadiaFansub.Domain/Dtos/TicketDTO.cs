using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Dtos
{
    public class TicketDTO
    {
        public string TicketId { get; set; } = null!;
        public string TicketTitle { get; set; }=null!;
        public string TicketMessage { get; set; }=null!;
        public string SenderName { get; set; }=null!;
        public string TicketDate { get; set; } = null!;
        public string TicketReason { get; set; }=null!;
        public string TicketStatus { get; set; }=null!;
        public DateTime TicketDateCreated { get; set;}
    }
}
