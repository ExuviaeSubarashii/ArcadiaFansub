using ArcadiaFansub.Domain.RequestDtos.TicketRequest;
using ArcadiaFansub.Domain.RequestDtos.UserRequest;
using ArcadiaFansub.Services.Services.TicketServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ArcadiaFansub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(TicketHandler TH) : ControllerBase
    {
        [HttpPost("CreateTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] TicketBody ticketBody, CancellationToken cancellationToken)
        {

            return (await TH.CreateTicket(ticketBody, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }

        [HttpPost("DeleteTicket/{ticketId}")]
        public async Task<IActionResult> DeleteTicket(string ticketId, CancellationToken cancellationToken)
        {
            return (await TH.DeleteTicket(ticketId, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }

        [HttpGet("GetAllTickets")]
        public async Task<IActionResult> GetAllTickets(CancellationToken cancellationToken)
        {
            return (await TH.GetAllTickets(cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPost("GetAllTicketsByUser")]
        public async Task<IActionResult> GetAllTicketsByUser([FromBody] UserAuthRequest request, CancellationToken cancellationToken)
        {
            return (await TH.GetUserSpecificTickets(request.UserToken, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }

        [HttpPost("GetSpecificTicket/{ticketId}")]
        public async Task<IActionResult> GetSpecificTickets([FromBody] UserAuthRequest request, string ticketId, CancellationToken cancellationToken)
        {
            if (await TicketAuth.IsTicketCreator(request.UserToken, ticketId) == true)
            {
                return Ok(await TH.GetSpecificTicket(ticketId, cancellationToken));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("GetTicketByType/{ticketType}")]
        public async Task<IActionResult> GetTicketByType([FromBody] UserAuthRequest request, string ticketType, CancellationToken cancellationToken)
        {
            return (await TH.GetByFilter(ticketType, request.UserToken, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }

        [HttpPost("GetTicketReply/{ticketId}")]
        public async Task<IActionResult> GetTicketReplies(string ticketId, CancellationToken cancellationToken)
        {
            return (await TH.GetTicketReply(ticketId, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }

        [HttpPost("GetTicketsBySearch/{ticketInput}")]
        public async Task<IActionResult> GetAllTicketsSearch([FromBody] UserAuthRequest request, string ticketInput, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(ticketInput))
            {
                return (await TH.GetTicketsBySearch(ticketInput, request.UserToken, cancellationToken)) is { } result ? Ok(result) : NotFound();
            }
            else
            {
                return Ok();
            }
        }
        [HttpPost("DeleteAdminResponse")]
        public async Task<IActionResult> DeleteAdminResponse([FromBody] DeleteAdminResponseBody request, CancellationToken cancellationToken)
        {
            return (await TH.DeleteAdminResponse(request, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
        [HttpPut("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicketBody ticketBody, CancellationToken cancellationToken)
        {
            return (await TH.UpdateTicket(ticketBody, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }

        [HttpPost("CreateAdminResponse")]
        public async Task<IActionResult> CreateAdminResponse([FromBody] AdminTicketResponseBody replyBody, CancellationToken cancellationToken)
        {
            return (await TH.CreateAdminResponse(replyBody, cancellationToken)) is { } result ? Ok(result) : NotFound();
        }
    }
}
