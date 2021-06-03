using Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.SignalR
{
    class SignalRTransferModel : IDataTransferModel
    {
        public HubConnection Connection;
        public event Action<ChatMessage> RecievedMessage;


        public SignalRTransferModel(string url)
        {
            Connection = new HubConnectionBuilder().WithUrl(url).WithAutomaticReconnect().Build();
            var v = Connection.StartAsync();
            Console.WriteLine(v.Exception);

            Connection.On<ChatMessage>("RecieveChatMessage", (message) => RecievedMessage?.Invoke(message));
        }

        public async Task<T> LoadData<T>(string url)
        {
            throw new NotImplementedException();
        }


        public async Task<string> SendData<T>(T data, string url)
        {
            await Connection.SendAsync("SendChatMessage", data);
            return "Send message";
        }
    }
}
