using ServerSide.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ServerSide.Controllers
{
    public class FilesController : ApiController
    {
        // GET api/values/5
        public HttpResponseMessage Get(string fileID)
        {
            string fileName = fileID;
            string localFilePath;

            localFilePath = fileName;

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        // POST api/values
        public async Task<string> Post()
        {

            string fullPath = HttpContext.Current.Server.MapPath("~/uploads");
            fullPath += "\\" + Request.Content.Headers.ContentDisposition.FileName;
            Stream fileStream = await Request.Content.ReadAsStreamAsync();


            ChatMessage message = new ChatMessage(new Models.User("server", null), "File is being uploaded: " + fullPath, false, fullPath);
            MockDatabase.Messages.Add(message);

            using (Stream file = File.Create(fullPath))
            {
                CopyStream(fileStream, file);
            }

            message = new ChatMessage(new Models.User("server", null), "File uploaded as " + fullPath, true, fullPath);
            MockDatabase.Messages.Add(message);
            return ("File uploaded as " + fullPath);

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
