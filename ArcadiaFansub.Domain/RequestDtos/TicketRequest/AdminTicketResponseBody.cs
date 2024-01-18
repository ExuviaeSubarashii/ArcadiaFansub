using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.TicketRequest
{
    public class AdminTicketResponseBody
    {
        public required string TicketId { get; set; }
        public required string AdminName { get; set; }
        public required string AdminReply { get; set; }
    }
}
