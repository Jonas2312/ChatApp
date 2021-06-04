using Domain.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    class SignalRTransferModel : IDataTransferModel
    {
        public HubConnection Connection;
        public event Action<ChatMessage> RecievedMessage;

        HttpClient HttpClient;


        public SignalRTransferModel(string url) : this(url, new HttpClient())
        {
        }

        public SignalRTransferModel(string url, HttpClient httpClient)
        {
            Connection = new HubConnectionBuilder().WithUrl(url).WithAutomaticReconnect().Build();
            var v = Connection.StartAsync();
            Console.WriteLine(v.Exception);

            Connection.On<ChatMessage>("RecieveChatMessage", (message) => RecievedMessage?.Invoke(message));
            HttpClient = httpClient;
        }

        public async Task<T> LoadData<T>(string url)
        {
            return default(T);
        }


        public async Task<string> SendData<T>(T data, string url)
        {
            await Connection.SendAsync("SendChatMessage", data);
            return "Send message";
        }
    }
}
