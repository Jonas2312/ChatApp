using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Models;

namespace ServerSide.Models
{
    public static class MockDatabase
    {

        public static List<ChatMessage> Messages = new List<ChatMessage>();
        public static List<User> Users = new List<User>();


        public static void FillDatabase()
        {
            Users.Add(new User("TestUser", "password"));
        }
    }
}