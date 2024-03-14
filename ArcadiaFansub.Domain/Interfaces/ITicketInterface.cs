using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.RequestDtos.TicketRequest;

namespace ArcadiaFansub.Domain.Interfaces
{
    public interface ITicketInterface
    {
        Task<IEnumerable<TicketDto>> GetAllTickets(CancellationToken cancellationToken);
        Task<IEnumerable<TicketDto>> GetTicketsBySearch(string ticketInpt, string userToken, CancellationToken cancellationToken);
        Task<TicketDto> GetSpecificTicket(string ticketId, CancellationToken cancellationToken);
        Task<IEnumerable<TicketReplyDto>> GetTicketReply(string ticketId, CancellationToken cancellationToken);
        Task<string> DeleteTicket(string ticketId, CancellationToken cancellationToken);
        Task<string> CreateTicket(TicketBody ticketCreateBody, CancellationToken cancellationToken);
        Task<string> UpdateTicket(UpdateTicketBody ticketUpdateBody, CancellationToken cancellationToken);
        Task<IEnumerable<TicketDto>> GetByFilter(string filter, string userToken, CancellationToken cancellationToken);
        Task<string> CreateAdminResponse(AdminTicketResponseBody adminBody, CancellationToken cancellationToken);
        Task<string> DeleteAdminResponse(DeleteAdminResponseBody body, CancellationToken cancellationToken);
        Task<IEnumerable<TicketDto>> GetUserSpecificTickets(string userToken, CancellationToken cancellationToken);
    }
}
