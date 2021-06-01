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

namespace Client.Model
{
    public static class ChatModel
    {
        private static HttpClient client = new HttpClient();

        public static User CurrentUser;

        public static async void WriteMessage(ChatMessage message)
        {
            string s = JsonConvert.SerializeObject(message);
            using (var stringContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json"))
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync("https://localhost:44339/api/Messages", stringContent);
                    var result = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        public static async void SetFile(byte[] fileArray, String fileName)
        {
            String serviceUrl = "https://localhost:44339/api/Files";
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    using (var content = new MultipartFormDataContent())
                    {
                        var fileContent = new ByteArrayContent(fileArray);//(System.IO.File.ReadAllBytes(fileName));
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = fileName
                        };
                        content.Add(fileContent);
                        var result = client.PostAsync(serviceUrl, content).Result;
                    }
                }
            }
            catch (Exception e)
            {
                //Log the exception
            }
        }

        public static async Task<string> LoadMessages()
        {
            var v = await client.GetAsync("https://localhost:44339/api/Messages");
            var w = await v.Content.ReadAsStringAsync();

            return w;
        }

        public static async void LoadFile(string filePath, ChatMessage message)
        {
            Console.WriteLine("Get files...");

            var v = await client.GetAsync("https://localhost:44339/api/Files?fileID=" + message.FileID);            
            var w = await v.Content.ReadAsStreamAsync();

            using (Stream file = File.Create(filePath))
            {
                CopyStream(w, file);
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }
    }
}
