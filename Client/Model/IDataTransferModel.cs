using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public interface IDataTransferModel
    {
        Task<T> LoadData<T>(string url);
        Task<string> SendData<T>(T data, string url);
    }
}
