using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Models
{
    public class User
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonConstructor]
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public User()
        {
            Name = "Parameterless User";
            Password = "Parameterless Password";
        }
    }
}