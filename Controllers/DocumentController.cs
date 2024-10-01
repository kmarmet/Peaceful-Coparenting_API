using Flurl;
using Api.Models;
using Mammoth;
using System.IO;
using System;
using System.Text;
using Firebase.Storage;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IConfiguration configuration;

        public DocumentController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        [HttpGet]
        public string Ping()
        {
            return "PING";
        }

        [HttpGet]
        public async Task<string> GetDocText(string fileName, string currentUserId)
        {
            var htmlResult = "";
            try
            {
                var bucket = configuration.GetValue<string>("DocumentStorageBucket");
                FirebaseStorage storage = new(bucket);
                var url = await storage.Child("documents").Child(currentUserId).Child(fileName).GetDownloadUrlAsync();
                var currentDirectory = Directory.GetCurrentDirectory();
                var path = $"{currentDirectory}/docs/{fileName}";

                System.IO.File.Delete(path);

                // Download File
                using (var client = new HttpClient())
                {
                    var fileResult = await client.GetAsync(url);
                    using (var fs = new FileStream(path, FileMode.CreateNew))
                    {
                        await fileResult.Content.CopyToAsync(fs);
                    }
                    client.Dispose();
                }

                // Convert to HTML
                var converter = new DocumentConverter();
                var result = converter.ConvertToHtml(path);

                // Delete file
                System.IO.File.Delete(path);

                htmlResult =  result.Value;
            }
            catch (Exception error)
            {
                var test = error.Message;
            }

            return htmlResult;
        }
    }
}