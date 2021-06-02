using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.SignalR
{
    class SignalRTransferModel : IDataTransferModel, IFileTransferModel
    {
        public void DownloadFile(string localFilePath, string remoteFileName, string url)
        {
            throw new NotImplementedException();
        }

        public Task<T> LoadData<T>(string url)
        {
            throw new NotImplementedException();
        }

        public void UploadFile(string localFilePath, string remoteFileName, string url)
        {
            throw new NotImplementedException();
        }

        public Task<string> SendData<T>(T data, string url)
        {
            throw new NotImplementedException();
        }
    }
}
