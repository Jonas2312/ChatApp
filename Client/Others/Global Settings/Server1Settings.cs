using Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Others.Global_Settings
{
    public static class Server1Settings
    {
        public static void Init()
        {
            Globals.Url = "https://localhost:44336";

            Globals.UserDataTransferModel = new RESTTransferModel();
            Globals.MessageDataTransferModel = new SignalRTransferModel(Globals.Url + "/chat");
            Globals.FileTransferModel = new RESTTransferModel();
        }
    }
}
