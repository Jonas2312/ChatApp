using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public static class StaticData
    {
        public static HttpClient client = new HttpClient();

        public static User CurrentUser;

        public static string Url = https://localhost:44339/;
    }
}
