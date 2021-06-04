using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendChatMessage(ChatMessage message)
        {
            MockDatabase.Messages.Add(message);
            Clients.All.SendAsync("RecieveChatMessage", message);
        }
    }
}
