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
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;
using System.Web.Http;
using WebApplication2.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication2.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public FilesController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }


        // GET api/values/5
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<FileStreamResult> Get(string fileID)
        {
            var path = fileID;
            var stream = System.IO.File.OpenRead(path);
            return new FileStreamResult(stream, "application/pdf");
        }

        // POST api/values
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<string> Post()
        {
            string fullPath = System.IO.Directory.GetCurrentDirectory();//HttpContext.Current.Server.MapPath("~/uploads");
            fullPath += "\\" + "Testfile.txt";


            ChatMessage message = new ChatMessage(new User("server", null), "File is being uploaded: " + fullPath, false, fullPath);

            MockDatabase.Messages.Add(message);
            await _hubContext.Clients.All.SendAsync("RecieveChatMessage", message);

            using (Stream file = System.IO.File.Create(fullPath))
            {
                byte[] buffer = new byte[8 * 1024];
                int len;
                while ((len = await Request.Body.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    file.Write(buffer, 0, len);
                }
            }

            message = new ChatMessage(new User("server", null), "File uploaded as " + fullPath, true, fullPath);
            MockDatabase.Messages.Add(message);
            await _hubContext.Clients.All.SendAsync("RecieveChatMessage", message);

            return ("File uploaded as " + fullPath);

        }


    }
}
