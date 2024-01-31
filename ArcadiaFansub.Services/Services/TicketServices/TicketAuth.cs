using ArcadiaFansub.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.TicketServices
{
    public class TicketAuth()
    {
        public static async Task<bool> IsTicketCreator(string userToken, string ticketId)
        {
            using ArcadiaFansubContext AF = new ArcadiaFansubContext();

            var ticketQuery = await AF.UserTickets.FirstOrDefaultAsync(x => x.TicketId == ticketId.Trim());
            var userQuery = await AF.Users.FirstOrDefaultAsync(x => x.UserToken == userToken.Trim());
            if (ticketQuery != null&&userQuery!=null)
            {
                if (ticketQuery.SenderToken == userToken|| userQuery.UserPermission == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
