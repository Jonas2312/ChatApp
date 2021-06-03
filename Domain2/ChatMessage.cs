using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Models
{
    public class ChatMessage
    {
        [JsonProperty("author")]
        public User Author { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("isFile")]
        public bool IsFile { get; set; }
        [JsonProperty("fileID")]
        public string FileID { get; set; }


        //public ChatMessage(User author, string content)
        //{
        //    Author = author;
        //    Content = content;
        //    IsFile = false;
        //    FileID = " ";
        //}

        [JsonConstructor]
        public ChatMessage(User author, string content, bool isFile, string fileID)
        {
            Author = author;
            Content = content;
            IsFile = isFile;
            FileID = fileID;
        }

        public ChatMessage()
        {
            Author = new User("ParamterelessUser", "Parameterless Password");
            Content = "Parameterless Content";
            IsFile = false;
            FileID = "Parameterless FileID";
        }
    }
}