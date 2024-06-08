using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _apiUser;
        private readonly HttpClient _client;

        public UserController(IConfiguration config)
        {
            _config = config;
            _apiUser = _config.GetValue<string>("ApiSettings:ApiUrl") + "User/";
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            List<User> list = new List<User>();
            var request = new HttpRequestMessage(HttpMethod.Get, _apiUser);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<User>>(jstring);
                return View(list);
            }
            else
                return View(list);
        }
    }
}
