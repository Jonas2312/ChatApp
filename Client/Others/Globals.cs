using Client.Model;
using Client.Model.SignalR;
using Domain.Models;

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


        public static string Url = "https://localhost:44336";

        public static IDataTransferModel UserDataTransferModel;
        public static IDataTransferModel MessageDataTransferModel;
        public static IFileTransferModel FileTransferModel;

        public static void Init()
        {
            UserDataTransferModel = new RESTTransferModel();
            MessageDataTransferModel = new SignalRTransferModel(Url + "/chat");
            FileTransferModel = new RESTTransferModel();
        }
    

    }
}
