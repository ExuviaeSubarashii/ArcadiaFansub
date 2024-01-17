using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Models
{
    public class AdminTicket
    {
        public string TicketId { get; set; } = null!;
        public string TicketAdminName { get; set; } = null!;
        public string TicketReply { get; set; } = null!;
        public DateTime TicketReplyDate { get; set; }
    }
}
