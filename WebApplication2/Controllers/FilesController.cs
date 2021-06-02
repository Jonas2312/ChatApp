using Microsoft.AspNetCore.Http;
using WebApplication2.Models;
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

namespace WebApplication2.Controllers
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
        public Task<IEnumerable<string>> Post()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                string fullPath = (string)AppDomain.CurrentDomain.GetData("ContentRootPath");
                fullPath += ("/uploads");

                MyMultipartFormDataStreamProvider streamProvider = new MyMultipartFormDataStreamProvider(fullPath);
                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t =>
                {
                    //if (t.IsFaulted || t.IsCanceled)
                    //    throw new HttpResponseException(HttpStatusCode.InternalServerError);

                    var fileInfo = streamProvider.FileData.Select(i =>
                    {
                        var info = new FileInfo(i.LocalFileName);
                        ChatMessage message = new ChatMessage(new Models.User("server", null), "File uploaded as " + info.FullName + "(" + info.Length + ")", true, info.FullName);
                        MockDatabase.Messages.Add(message);
                        return "File uploaded as " + info.FullName + " (" + info.Length + ")";
                    });
                    return fileInfo;

                });
                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "Invalid Request!"));
            }
        }

        public class MyMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            public MyMultipartFormDataStreamProvider(string path)
                : base(path)
            {

            }

            public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
            {
                string fileName;
                if (!string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName))
                {
                    fileName = headers.ContentDisposition.FileName;
                }
                else
                {
                    fileName = Guid.NewGuid().ToString() + ".data";
                }
                return fileName.Replace("\"", string.Empty);
            }
        }

    }
}
