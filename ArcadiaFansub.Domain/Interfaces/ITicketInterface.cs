using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.TicketRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface ITicketInterface
    {
        Task<IEnumerable<TicketDTO>> GetAllTickets();
        Task<IEnumerable<TicketDTO>> GetTicketsBySearch(string ticketInpt);
        Task<TicketDTO> GetSpecificTicket(string ticketId);
        Task<IEnumerable<TicketReplyDTO>> GetTicketReply(string ticketId);
        Task<string> DeleteTicket(string ticketId);
        Task<string> CreateTicket(TicketBody ticketCreateBody);
        Task<string> UpdateTicket(UpdateTicketBody ticketUpdateBody);
        Task<IEnumerable<TicketDTO>> GetByFilter(string filter);
        Task<string> CreateAdminResponse(AdminTicketResponseBody adminBody);
    }
}
