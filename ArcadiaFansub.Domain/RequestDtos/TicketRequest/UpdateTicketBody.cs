using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.TicketRequest
{
    public class UpdateTicketBody
    {
        public required string TicketId { get; set; }
        public required string TicketStatus { get; set; }
    }
}
