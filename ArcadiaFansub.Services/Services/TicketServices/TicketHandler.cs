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
    public class TicketHandler(ArcadiaFansubContext AF) : ITicketInterface
    {
        public async Task<string> CreateAdminResponse(AdminTicketResponseBody adminBody)
        {
            AdminTicket newAdminTicket = new()
            {
                TicketAdminName = adminBody.AdminName,
                TicketId = adminBody.TicketId,
                TicketReply = adminBody.AdminReply,
                TicketReplyDate = DateTime.Now
            };
            AF.AdminTickets.Add(newAdminTicket);
            await AF.SaveChangesAsync();
            return "Succesfully created response";
        }

        public async Task<string> CreateTicket(TicketBody ticketCreateBody)
        {
            UserTicket newTicket = new()
            {
                TicketId = Guid.NewGuid().ToString("D"),
                SenderName = ticketCreateBody.SenderName,
                TicketDate = DateTime.Now,
                TicketMessage = ticketCreateBody.TicketMessage,
                TicketReason = ticketCreateBody.TicketReason,
                TicketStatus = ticketCreateBody.TicketStatus,
                TicketTitle = ticketCreateBody.TicketTitle
            };
            AF.UserTickets.Add(newTicket);
            await AF.SaveChangesAsync();
            return "Ticket create Succesfully";
        }

        public Task<string> DeleteAdminResponse(DeleteAdminResponseBody body)
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeleteTicket(string ticketId)
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
                AF.SaveChanges();
                return $"Ticket {ticketId} successfully deleted";
            }
            catch (Exception)
            {

                throw new ArgumentException("Could not delete ticket");
            }

        }

        public async Task<IEnumerable<TicketDTO>> GetAllTickets()
        {
            var allTickets = await AF.UserTickets.Select(ticket => new TicketDTO
            {
                SenderName = ticket.SenderName.Trim(),
                TicketDate = EpisodeHandler.GetDate(ticket.TicketDate),
                TicketId = ticket.TicketId.ToString().Trim(),
                TicketMessage = ticket.TicketMessage.Trim(),
                TicketReason = ticket.TicketReason.Trim(),
                TicketStatus = ticket.TicketStatus.Trim(),
                TicketTitle = ticket.TicketTitle.Trim(),
                TicketDateCreated = ticket.TicketDate
            }).ToListAsync();
            if (allTickets.Any())
            {
                return allTickets.OrderBy(e => e.TicketDateCreated);
            }
            else
            {
                return new List<TicketDTO>();
            }
        }

        public async Task<IEnumerable<TicketDTO>> GetByFilter(string filter)
        {
            var allTickets = await AF.UserTickets.Where(x => x.TicketReason == filter.Trim()).Select(ticket => new TicketDTO
            {
                SenderName = ticket.SenderName.Trim(),
                TicketDate = EpisodeHandler.GetDate(ticket.TicketDate),
                TicketId = ticket.TicketId.ToString().Trim(),
                TicketMessage = ticket.TicketMessage.Trim(),
                TicketReason = ticket.TicketReason.Trim(),
                TicketStatus = ticket.TicketStatus.Trim(),
                TicketTitle = ticket.TicketTitle.Trim(),
                TicketDateCreated = ticket.TicketDate
            }).ToListAsync();
            if (allTickets.Any())
            {
                return allTickets.OrderBy(e => e.TicketDateCreated);
            }
            else
            {
                return new List<TicketDTO>();
            }
        }

        public async Task<TicketDTO> GetSpecificTicket(string ticketId)
        {
            var allTickets = await AF.UserTickets.Where(x => x.TicketId == ticketId).Select(ticket => new TicketDTO
            {
                SenderName = ticket.SenderName.Trim(),
                TicketDate = ticket.TicketDate.ToShortDateString().Trim(),
                TicketId = ticket.TicketId.Trim(),
                TicketMessage = ticket.TicketMessage.Trim(),
                TicketReason = ticket.TicketReason.Trim(),
                TicketStatus = ticket.TicketStatus.Trim(),
                TicketTitle = ticket.TicketTitle.Trim(),
                TicketDateCreated = ticket.TicketDate
            }).FirstOrDefaultAsync();
            if (allTickets != null)
            {
                return allTickets;
            }
            else
            {
                return new TicketDTO();
            }
        }

        public async Task<IEnumerable<TicketReplyDTO>> GetTicketReply(string ticketId)
        {
            var ticketReplies = await AF.AdminTickets.Where(id => id.TicketId == ticketId.Trim()).Select(x => new TicketReplyDTO
            {
                ResponseId = x.ResponseId,
                TicketId = ticketId,
                TicketAdminName = x.TicketAdminName,
                TicketReply = x.TicketReply,
                TicketReplyDate = x.TicketReplyDate
            }).ToListAsync();
            if (ticketReplies != null)
            {
                return ticketReplies;
            }
            else
            {
                return new List<TicketReplyDTO>();
            }
        }

        public async Task<IEnumerable<TicketDTO>> GetTicketsBySearch(string ticketInput)
        {
            var ticketBySearch = await AF.UserTickets.Where(x => x.TicketTitle.StartsWith(ticketInput)).Select(x => new TicketDTO
            {
                TicketTitle = x.TicketTitle,
                SenderName = x.SenderName,
                TicketDate = EpisodeHandler.GetDate(x.TicketDate),
                TicketDateCreated = x.TicketDate,
                TicketId = x.TicketId,
                TicketMessage = x.TicketMessage,
                TicketReason = x.TicketReason,
                TicketStatus = x.TicketStatus
            }).ToListAsync();
            if (ticketBySearch.Any())
            {
                return ticketBySearch;
            }
            else
            {
                return new List<TicketDTO>();
            }
        }

        public async Task<string> UpdateTicket(UpdateTicketBody ticketUpdateBody)
        {
            var ticketToUpdate = await AF.UserTickets.FirstOrDefaultAsync(ticket => ticket.TicketId == ticketUpdateBody.TicketId.Trim());
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
