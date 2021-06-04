using Client.Model;
using Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Others
{
    public abstract class Globals
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


        public static string Url;

        public static IDataTransferModel UserDataTransferModel;
        public static IDataTransferModel MessageDataTransferModel;
        public static IFileTransferModel FileTransferModel;      

    }
}
