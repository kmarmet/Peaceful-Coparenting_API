using Microsoft.AspNetCore.Mvc;
using Flurl.Http;
using Api.Models;
using Api.Emailer;
using Microsoft.AspNetCore.Http.Extensions;
namespace Api.Controllers {
    public class MessagingController(IConfiguration _configuration) : Controller {
        [HttpGet]
        public string Ping() {
            return "PING";
        }

        [HttpPost]
        public async Task<string> SendToAll([FromForm] SendToAllMessageBody messageBody) {
            var result = await "https://api.pushalert.co/rest/v1/send".WithHeaders(new {
                Authorization = $"api_key={_configuration.GetValue<string>("PushAlertApiKey")}"
            }).PostUrlEncodedAsync(messageBody);

            var response = await result.GetStringAsync();
            return response;
        }

        // POST: api/values
        [HttpPost]
        public async Task<string> SendMessage([FromForm] MessageBody messageBody) {
            var result = await "https://api.pushalert.co/rest/v1/send".WithHeaders(new {
                Authorization = $"api_key={_configuration.GetValue<string>("PushAlertApiKey")}"
            }).PostUrlEncodedAsync(messageBody);

            var response = await result.GetStringAsync();
            // Console.WriteLine(response);
            return response;
        }
    }
}