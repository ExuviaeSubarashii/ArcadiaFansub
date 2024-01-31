using ArcadiaFansub.Domain.RequestDtos.TicketRequest;
using ArcadiaFansub.Domain.RequestDtos.UserRequest;
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
        public async Task<IActionResult> CreateTicket([FromBody] TicketBody ticketBody)
        {
            if (ticketBody is TicketBody)
            {
                return Ok(await TH.CreateTicket(ticketBody));
            }
            else
            {
                return BadRequest("Something is Missing");
            }
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
        [HttpPost("GetAllTicketsByUser")]
        public async Task<IActionResult> GetAllTicketsByUser([FromBody] UserAuthRequest request)
        {
            return Ok(await TH.GetUserSpecificTickets(request.UserToken));
        }

        [HttpPost("GetSpecificTicket/{ticketId}")]
        public async Task<IActionResult> GetSpecificTickets([FromBody] UserAuthRequest request, string ticketId)
        {
            if (await TicketAuth.IsTicketCreator(request.UserToken, ticketId) == true)
            {
                return Ok(await TH.GetSpecificTicket(ticketId));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("GetTicketByType/{ticketType}")]
        public async Task<IActionResult> GetTicketByType([FromBody] UserAuthRequest request, string ticketType)
        {
            return Ok(await TH.GetByFilter(ticketType, request.UserToken));
        }

        [HttpPost("GetTicketReply/{ticketId}")]
        public async Task<IActionResult> GetTicketReplies(string ticketId)
        {
            return Ok(await TH.GetTicketReply(ticketId));
        }

        [HttpPost("GetTicketsBySearch/{ticketInput}")]
        public async Task<IActionResult> GetAllTicketsSearch([FromBody]UserAuthRequest request, string ticketInput)
        {
            if (!string.IsNullOrEmpty(ticketInput))
            {
                return Ok(await TH.GetTicketsBySearch(ticketInput,request.UserToken));
            }
            else
            {
                return Ok();
            }
        }
        [HttpPost("DeleteAdminResponse")]
        public async Task<IActionResult> DeleteAdminResponse([FromBody] DeleteAdminResponseBody request)
        {
            return Ok(await TH.DeleteAdminResponse(request));
        }
        [HttpPut("UpdateTicket")]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicketBody ticketBody)
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
