using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
            var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";
            var userName = HttpContext.Session.GetString("User");
            
            if(userName == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!isAdmin)
            {
                return RedirectToAction("Home", "Home");
            }

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

        public async Task<IActionResult> Delete(int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _apiUser + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return View();
        }

        public IActionResult Add()
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";

            if (!isAdmin)
            {
                return RedirectToAction("Home", "Home");
            }

            var user = new User();
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _apiUser);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage message = await _client.SendAsync(request);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(user);
                }

            }
            else
            {
                return View(user);
            }
        }

        public async Task<IActionResult> Update(int Id)
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";

            if (!isAdmin)
            {
                return RedirectToAction("Home", "Home");
            }

            var request = new HttpRequestMessage(HttpMethod.Get, _apiUser + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                var details = JsonConvert.DeserializeObject<User>(jstring);
                return View(details);
            }
            else
                return RedirectToAction("Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Put, _apiUser);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage message = await _client.SendAsync(request);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(user);
                }

            }
            else
            {
                return View(user);
            }
        }

        public IActionResult Back(string controller)
        {
            return RedirectToAction("Index", controller);
        }
    }
}
