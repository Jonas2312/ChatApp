using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerSide.Models
{
    public static class MockDatabase
    {

        public static List<ChatMessage> Messages = new List<ChatMessage>();
        public static List<User> Users = new List<User>();
    }
}