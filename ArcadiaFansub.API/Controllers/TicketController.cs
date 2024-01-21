using ArcadiaFansub.Domain.RequestDtos.TicketRequest;
using ArcadiaFansub.Services.Services.TicketServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(TicketHandler TH) : ControllerBase
    {
        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket([FromBody]TicketBody ticketBody)
        {
            return Ok(await TH.CreateTicket(ticketBody));
        }

        [HttpPost("DeleteTicket/{ticketId}")]
        public async Task<IActionResult> DeleteTicket(string ticketId)
        {
            return Ok(await TH.DeleteTicket(ticketId));
        }

        [HttpGet("GetAllTickets")]
        public async Task<IActionResult> GetAllTickets()
        {
            return Ok(await TH.GetAllTickets());
        }

        [HttpPost("GetSpecificTicket/{ticketId}")]
        public async Task<IActionResult> GetSpecificTickets(string ticketId)
        {
            return Ok(await TH.GetSpecificTicket(ticketId));
        }

        [HttpPost("GetTicketByType/{ticketType}")]
        public async Task<IActionResult> GetTicketByType(string ticketType)
        {
            return Ok(await TH.GetByFilter(ticketType));
        }

        [HttpPost("GetTicketReply/{ticketId}")]
        public async Task<IActionResult> GetTicketReplies(string ticketId)
        {
            return Ok(await TH.GetTicketReply(ticketId));
        }

        [HttpPost("GetTicketsBySearch/{ticketInput}")]
        public async Task<IActionResult> GetAllTicketsSearch(string ticketInput)
        {
            return Ok(await TH.GetTicketsBySearch(ticketInput));
        }

        [HttpPut("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket([FromBody]UpdateTicketBody ticketBody)
        {
            return Ok(await TH.UpdateTicket(ticketBody));
        }

        [HttpPost("CreateAdminResponse")]
        public async Task<IActionResult> CreateAdminResponse([FromBody] AdminTicketResponseBody replyBody)
        {
            return Ok(await TH.CreateAdminResponse(replyBody));
        }
    }
}
