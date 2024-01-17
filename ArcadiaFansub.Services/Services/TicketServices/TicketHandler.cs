using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.TicketRequest;
using ArcadiaFansub.Services.Services.EpisodeServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.TicketServices
{
    public class TicketHandler(ArcadiaFansubContext AF) : ITicketInterface
    {
        public async Task<string> CreateTicket(TicketBody ticketCreateBody)
        {
            UserTicket newTicket = new()
            { 
                TicketId = Guid.NewGuid().ToString("D"),
                SenderName = ticketCreateBody.SenderName,
                TicketDate = DateTime.Today,
                TicketMessage = ticketCreateBody.TicketMessage,
                TicketReason = ticketCreateBody.TicketReason,
                TicketStatus = ticketCreateBody.TicketStatus,
                TicketTitle = ticketCreateBody.TicketTitle
            };
            AF.UserTickets.Add(newTicket);
            await AF.SaveChangesAsync();
            return "Ticket create Succesfully";
        }

        public async Task<string> DeleteTicket(string ticketId)
        {
            var ticketToDelete = await AF.UserTickets.FirstOrDefaultAsync(x => x.TicketId == ticketId.Trim());
            AF.Remove(ticketToDelete);
            AF.SaveChanges();
            return $"Ticket {ticketId} successfully deleted";
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
                return allTickets.OrderBy(e=>e.TicketDateCreated);
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
                SenderName = ticket.SenderName,
                TicketDate = ticket.TicketDate.ToShortDateString(),
                TicketId = ticket.TicketId,
                TicketMessage = ticket.TicketMessage,
                TicketReason = ticket.TicketReason,
                TicketStatus = ticket.TicketStatus,
                TicketTitle = ticket.TicketTitle,
                TicketDateCreated=ticket.TicketDate
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
                TicketId = ticketId,
                TicketAdminName = x.TicketAdminName,
                TicketReply = x.TicketReply,
                TicketReplyDate = x.TicketReplyDate
            }).ToListAsync();
            if (ticketReplies!=null)
            {
                return ticketReplies;
            }
            else
            {
                return new List<TicketReplyDTO>();
            }
        }

        public Task<IEnumerable<TicketDTO>> GetTicketsBySearch(string ticketInput)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateTicket(TicketBody ticketUpdateBody)
        {
            throw new NotImplementedException();
        }
    }
}
