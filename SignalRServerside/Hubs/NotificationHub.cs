using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRServerside.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessages(string message)
        {
            await Clients.All.SendAsync("RecieveMessage", message);
        }
    }
}
