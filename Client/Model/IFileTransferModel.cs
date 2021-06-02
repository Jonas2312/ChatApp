using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public interface IFileTransferModel
    {
        void DownloadFile(string localFilePath, string remoteFileName, string url);
        void UploadFile(string localFilePath, string remoteFileName, string url);
    }
}
