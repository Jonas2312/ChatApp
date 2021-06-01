using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerSide.Models
{
    public class ChatMessage
    {
        [JsonProperty("user")]
        public User Author { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("isFile")]
        public bool IsFile { get; set; }
        [JsonProperty("fileID")]
        public bool FileID { get; set; }


        public ChatMessage(User user, string content)
        {
            Author = user;
            Content = content;
        }
    }
}