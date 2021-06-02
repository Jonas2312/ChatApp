using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ChatMessage
    {
        [JsonProperty("author")]
        public User Author { get; }
        [JsonProperty("content")]
        public string Content { get; }
        [JsonProperty("isFile")]
        public bool IsFile { get; }
        [JsonProperty("fileID")]
        public string FileID { get; }


        //public ChatMessage(User author, string content)
        //{
        //    Author = author;
        //    Content = content;
        //    IsFile = false;
        //    FileID = " ";
        //}


        public ChatMessage(User author, string content, bool isFile, string fileID)
        {
            Author = author;
            Content = content;
            IsFile = isFile;
            FileID = fileID;
        }
    }
}