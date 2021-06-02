using Client.Model;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Others
{
    public static class Globals
    {
        private static User currentUser;
        public static User CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }


        public static IDataTransferModel dataTransferModel = new RESTTransferModel();
        public static IFileTransferModel fileTransferModel = new RESTTransferModel();

        public static string Url = "https://localhost:44339/";
    }
}
