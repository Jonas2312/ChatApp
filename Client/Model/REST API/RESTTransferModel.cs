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

        /// <summary>
        /// Recieves Data from url. Objects of type T are being expected to be recieved in json-format and deserializable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>        
        public async Task<T> LoadData<T>(string url)
        {
            var responseMessage = await HttpClient.GetAsync(url).ConfigureAwait(false);
            var loadedJSON = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            T data = JsonConvert.DeserializeObject<T>(loadedJSON);

            return data;
        }

        /// <summary>
        /// Send data to url. Data has to be of json-serializable Object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> SendData<T>(T data, string url)
        {
            string s = JsonConvert.SerializeObject(data);
            Console.WriteLine(s);
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            {
                try
                {
                    HttpResponseMessage response = await HttpClient.PostAsync(url, stringContent).ConfigureAwait(false);
                    string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return "Could not get a result from server";
        }

        /// <summary>
        /// Uses content property of HttpResponseMessage to recieve files from [url + "/api/Files"]
        /// </summary>
        /// <param name="localFilePath"></param>
        /// <param name="remoteFileName"></param>
        /// <param name="url"></param>
        public async void DownloadFile(string localFilePath, string remoteFileName, string url)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(url + "/api/Files?fileID=" + remoteFileName).ConfigureAwait(false);
            Stream fileStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            using (Stream file = File.Create(localFilePath))
            {
                Utils.CopyStream(fileStream, file);
            }
        }

        /// <summary>
        /// Uses content property of HttpResponseMessage to send files to [url + "/api/Files"]
        /// </summary>
        /// <param name="localFilePath"></param>
        /// <param name="remoteFileName"></param>
        /// <param name="url"></param>
        public async void UploadFile(string localFilePath, string remoteFileName, string url)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = remoteFileName;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            await HttpClient.PostAsync(url + "/api/Files", response.Content).ConfigureAwait(false);
        }


    }
}
