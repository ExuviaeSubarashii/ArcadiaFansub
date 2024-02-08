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
        Task<IEnumerable<TicketDTO>> GetAllTickets(CancellationToken cancellationToken);
        Task<IEnumerable<TicketDTO>> GetTicketsBySearch(string ticketInpt, string userToken, CancellationToken cancellationToken);
        Task<TicketDTO> GetSpecificTicket(string ticketId, CancellationToken cancellationToken);
        Task<IEnumerable<TicketReplyDTO>> GetTicketReply(string ticketId, CancellationToken cancellationToken);
        Task<string> DeleteTicket(string ticketId, CancellationToken cancellationToken);
        Task<string> CreateTicket(TicketBody ticketCreateBody, CancellationToken cancellationToken);
        Task<string> UpdateTicket(UpdateTicketBody ticketUpdateBody, CancellationToken cancellationToken);
        Task<IEnumerable<TicketDTO>> GetByFilter(string filter, string userToken, CancellationToken cancellationToken);
        Task<string> CreateAdminResponse(AdminTicketResponseBody adminBody, CancellationToken cancellationToken);
        Task<string> DeleteAdminResponse(DeleteAdminResponseBody body, CancellationToken cancellationToken);
        Task<IEnumerable<TicketDTO>> GetUserSpecificTickets(string userToken, CancellationToken cancellationToken);
    }
}
