using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.RequestDtos.TicketRequest
{
    public class DeleteAdminResponseBody
    {
        public required string UserToken { get; set; }
        public required int ResponseId { get; set; }
    }
}
