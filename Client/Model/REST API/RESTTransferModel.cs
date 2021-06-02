using Newtonsoft.Json;
using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Client.Others;
using System.Net;

namespace Client.Model
{
    public class RESTTransferModel : IDataTransferModel, IFileTransferModel
    {
        HttpClient HttpClient;

        public RESTTransferModel()
        {
            HttpClient = new HttpClient();
        }

        public RESTTransferModel(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

            
        public async Task<T> LoadData<T>(string url)
        {
            var responseMessage = await HttpClient.GetAsync(url);
            var loadedJSON = await responseMessage.Content.ReadAsStringAsync();
            T data = JsonConvert.DeserializeObject<T>(loadedJSON);

            return data;
        }


        public async void SendData<T>(T data, string url)
        {
            string s = JsonConvert.SerializeObject(data);
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {
                try
                {
                    var response = await HttpClient.PostAsync(url, stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }


        public async void DownloadFile(string localFilePath, string remoteFileName, string url)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(url + "/api/Files?fileID=" + remoteFileName);
            Stream fileStream = await response.Content.ReadAsStreamAsync();

            using (Stream file = File.Create(localFilePath))
            {
                Utils.CopyStream(fileStream, file);
            }
        }


        public async void UploadFile(string localFilePath, string remoteFileName, string url)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = remoteFileName;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            await HttpClient.PostAsync(url + "/api/Files", response.Content);
        }


    }
}
