using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Models;
using WebApplication2.Hubs;

namespace WebApplication2.Models
{
    public static class MockDatabase
    {
        private static List<ChatMessage> messages;

        public static List<ChatMessage> Messages {
            get
            {
                return messages;
            }
            set
            {
                messages = value;
            }
        }
        public static List<User> Users { get; set; }


        public static void FillDatabase()
        {
            Messages = new List<ChatMessage>();
            Users = new List<User>();
            Users.Add(new User("TestUser", "password"));
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              