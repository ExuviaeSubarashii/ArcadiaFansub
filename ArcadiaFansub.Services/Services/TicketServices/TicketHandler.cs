using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.TicketRequest;
using ArcadiaFansub.Services.Services.EpisodeServices;
using ArcadiaFansub.Services.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.TicketServices
{
    public class TicketHandler(ArcadiaFansubContext AF, UserAuthentication UA) : ITicketInterface
    {
        public async Task<string> CreateAdminResponse(AdminTicketResponseBody adminBody, CancellationToken cancellationToken)
        {
            AdminTicket newAdminTicket = new()
            {
                TicketAdminName = adminBody.AdminName,
                TicketId = adminBody.TicketId,
                TicketReply = adminBody.AdminReply,
                TicketReplyDate = DateTime.Now
            };
            AF.AdminTickets.Add(newAdminTicket);
            await AF.SaveChangesAsync(cancellationToken);
            return "Succesfully created response";
        }

        public async Task<string> CreateTicket(TicketBody ticketCreateBody, CancellationToken cancellationToken)
        {
            UserTicket newTicket = new()
            {
                TicketId = Guid.NewGuid().ToString("D"),
                SenderName = ticketCreateBody.SenderName,
                TicketDate = DateTime.Now,
                TicketMessage = ticketCreateBody.TicketMessage,
                TicketReason = ticketCreateBody.TicketReason,
                TicketStatus = ticketCreateBody.TicketStatus,
                TicketTitle = ticketCreateBody.TicketTitle,
                SenderToken = ticketCreateBody.SenderToken,
            };
            AF.UserTickets.Add(newTicket);
            await AF.SaveChangesAsync(cancellationToken);
            return "Ticket create Succesfully";
        }

        public async Task<string> DeleteAdminResponse(DeleteAdminResponseBody body, CancellationToken cancellationToken)
        {
            var adminResponse = await AF.AdminTickets.Where(x => x.ResponseId == body.ResponseId).FirstOrDefaultAsync();
            if (adminResponse != null)
            {
                AF.Remove(adminResponse);
                await AF.SaveChangesAsync(cancellationToken);
                return $"Deleted {adminResponse.TicketReply}";
            }
            else
            {
                return "Could not delete message";
            }

        }

        public async Task<string> DeleteTicket(string ticketId, CancellationToken cancellationToken)
        {
            try
            {
                var ticketToDelete = await AF.UserTickets.FirstOrDefaultAsync(x => x.TicketId == ticketId.Trim());
                var adminTicketsToDelete = await AF.AdminTickets.Where(x => x.TicketId == ticketId).ToListAsync();
                if (adminTicketsToDelete.Any())
                {
                    AF.RemoveRange(adminTicketsToDelete);
                }
                AF.Remove(ticketToDelete);
                await AF.SaveChangesAsync(cancellationToken);
                return $"Ticket {ticketId} successfully deleted";
            }
            catch (Exception)
            {

                throw new ArgumentException("Could not delete ticket");
            }

        }

        public async Task<IEnumerable<TicketDto>> GetAllTickets(CancellationToken cancellationToken)
        {
            var allTickets = await AF.UserTickets.Select(ticket => new TicketDto
            {
                SenderName = ticket.SenderName.Trim(),
                TicketDate = EpisodeHandler.GetDate(ticket.TicketDate),
                TicketId = ticket.TicketId.ToString().Trim(),
                TicketMessage = ticket.TicketMessage.Trim(),
                TicketReason = ticket.TicketReason.Trim(),
                TicketStatus = ticket.TicketStatus.Trim(),
                TicketTitle = ticket.TicketTitle.Trim(),
                TicketDateCreated = ticket.TicketDate
            }).ToListAsync(cancellationToken);
            if (allTickets.Any())
            {
                return allTickets.OrderBy(e => e.TicketDateCreated);
            }
            else
            {
                return new List<TicketDto>();
            }
        }

        public async Task<IEnumerable<TicketDto>> GetByFilter(string filter, string userToken, CancellationToken cancellationToken)
        {
            if (await UA.IsAdmin(userToken) == true)
            {
                var allTickets = await AF.UserTickets.Where(x => x.TicketReason == filter.Trim()).Select(ticket => new TicketDto
                {
                    SenderName = ticket.SenderName.Trim(),
                    TicketDate = EpisodeHandler.GetDate(ticket.TicketDate),
                    TicketId = ticket.TicketId.ToString().Trim(),
                    TicketMessage = ticket.TicketMessage.Trim(),
                    TicketReason = ticket.TicketReason.Trim(),
                    TicketStatus = ticket.TicketStatus.Trim(),
                    TicketTitle = ticket.TicketTitle.Trim(),
                    TicketDateCreated = ticket.TicketDate
                }).ToListAsync(cancellationToken);
                if (allTickets.Any())
                {
                    return allTickets.OrderBy(e => e.TicketDateCreated);
                }
                else
                {
                    return new List<TicketDto>();
                }
            }
            else
            {

                var allTickets = await AF.UserTickets.Where(x => x.TicketReason == filter.Trim() && x.SenderToken == userToken.Trim()).Select(ticket => new TicketDto
                {
                    SenderName = ticket.SenderName.Trim(),
                    TicketDate = EpisodeHandler.GetDate(ticket.TicketDate),
                    TicketId = ticket.TicketId.ToString().Trim(),
                    TicketMessage = ticket.TicketMessage.Trim(),
                    TicketReason = ticket.TicketReason.Trim(),
                    TicketStatus = ticket.TicketStatus.Trim(),
                    TicketTitle = ticket.TicketTitle.Trim(),
                    TicketDateCreated = ticket.TicketDate
                }).ToListAsync(cancellationToken);
                if (allTickets.Any())
                {
                    return allTickets.OrderBy(e => e.TicketDateCreated);
                }
                else
                {
                    return new List<TicketDto>();
                }
            }


        }

        public async Task<TicketDto> GetSpecificTicket(string ticketId, CancellationToken cancellationToken)
        {
            var allTickets = await AF.UserTickets.Where(x => x.TicketId == ticketId).Select(ticket => new TicketDto
            {
                SenderName = ticket.SenderName.Trim(),
                TicketDate = ticket.TicketDate.ToShortDateString().Trim(),
                TicketId = ticket.TicketId.Trim(),
                TicketMessage = ticket.TicketMessage.Trim(),
                TicketReason = ticket.TicketReason.Trim(),
                TicketStatus = ticket.TicketStatus.Trim(),
                TicketTitle = ticket.TicketTitle.Trim(),
                TicketDateCreated = ticket.TicketDate
            }).FirstOrDefaultAsync(cancellationToken);
            if (allTickets != null)
            {
                return allTickets;
            }
            else
            {
                return new TicketDto();
            }
        }

        public async Task<IEnumerable<TicketReplyDto>> GetTicketReply(string ticketId, CancellationToken cancellationToken)
        {
            var ticketReplies = await AF.AdminTickets.Where(id => id.TicketId == ticketId.Trim()).Select(x => new TicketReplyDto
            {
                ResponseId = x.ResponseId,
                TicketId = ticketId,
                TicketAdminName = x.TicketAdminName,
                TicketReply = x.TicketReply,
                TicketReplyDate = x.TicketReplyDate
            }).ToListAsync(cancellationToken);
            if (ticketReplies != null)
            {
                return ticketReplies;
            }
            else
            {
                return new List<TicketReplyDto>();
            }
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsBySearch(string ticketInput, string userToken, CancellationToken cancellationToken)
        {
            var ticketBySearch = await AF.UserTickets.Where(x => x.TicketTitle.StartsWith(ticketInput) && x.SenderToken == userToken.Trim()).Select(x => new TicketDto
            {
                TicketTitle = x.TicketTitle,
                SenderName = x.SenderName,
                TicketDate = EpisodeHandler.GetDate(x.TicketDate),
                TicketDateCreated = x.TicketDate,
                TicketId = x.TicketId,
                TicketMessage = x.TicketMessage,
                TicketReason = x.TicketReason,
                TicketStatus = x.TicketStatus
            }).ToListAsync(cancellationToken);
            if (ticketBySearch.Any())
            {
                return ticketBySearch;
            }
            else
            {
                return new List<TicketDto>();
            }
        }

        public async Task<IEnumerable<TicketDto>> GetUserSpecificTickets(string userToken, CancellationToken cancellationToken)
        {
            var userSpecificQuery = await AF.UserTickets.Where(x => x.SenderToken == userToken.Trim()).Select(ticket => new TicketDto
            {
                SenderName = ticket.SenderName.Trim(),
                TicketDate = EpisodeHandler.GetDate(ticket.TicketDate),
                TicketId = ticket.TicketId.ToString().Trim(),
                TicketMessage = ticket.TicketMessage.Trim(),
                TicketReason = ticket.TicketReason.Trim(),
                TicketStatus = ticket.TicketStatus.Trim(),
                TicketTitle = ticket.TicketTitle.Trim(),
                TicketDateCreated = ticket.TicketDate
            }).ToListAsync(cancellationToken);
            return userSpecificQuery;
        }

        public async Task<string> UpdateTicket(UpdateTicketBody ticketUpdateBody, CancellationToken cancellationToken)
        {
            var ticketToUpdate = await AF.UserTickets.FirstOrDefaultAsync(ticket => ticket.TicketId == ticketUpdateBody.TicketId.Trim(), cancellationToken);
            if (ticketToUpdate != null)
            {
                ticketToUpdate.TicketStatus = ticketUpdateBody.TicketStatus;
                AF.SaveChanges();
                return $"Ticket {ticketUpdateBody.TicketId} status succesfully changed.";
            }
            else
            {
                return $"Failed to Update ticket {ticketUpdateBody.TicketId}.";
            }
        }

    }
}
