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
        public async Task<ActionResult> CreateTicket([FromBody]TicketBody ticketBody)
        {
            return Ok(await TH.CreateTicket(ticketBody));
        }
        [HttpPost("DeleteTicket/{ticketId}")]
        public async Task<ActionResult> DeleteTicket(string ticketId)
        {
            return Ok(await TH.DeleteTicket(ticketId));
        }
        [HttpGet("GetAllTickets")]
        public async Task<ActionResult> GetAllTickets()
        {
            return Ok(await TH.GetAllTickets());
        }
        [HttpPost("GetSpecificTicket/{ticketId}")]
        public async Task<ActionResult> GetSpecificTickets(string ticketId)
        {
            return Ok(await TH.GetSpecificTicket(ticketId));
        }
        [HttpPost("GetTicketReply/{ticketId}")]
        public async Task<ActionResult> GetTicketReplies(string ticketId)
        {
            return Ok(await TH.GetTicketReply(ticketId));
        }
        [HttpPost("GetTicketsBySearch/{ticketInput}")]
        public async Task<ActionResult> GetAllTicketsSearch(string ticketInput)
        {
            return Ok(await TH.GetTicketsBySearch(ticketInput));
        }
        [HttpPut("UpdateTicket")]
        public async Task<ActionResult> UpdateTicket([FromBody]TicketBody ticketBody)
        {
            return Ok(await TH.UpdateTicket(ticketBody));
        }
    }
}
