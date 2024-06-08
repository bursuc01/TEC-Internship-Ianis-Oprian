using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DetailsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _apiDetails;
        private readonly HttpClient _client;
        public DetailsController(IConfiguration config)
        {
            _config = config;
            _apiDetails = _config.GetValue<string>("ApiSettings:ApiUrl") + "PersonDetails/";
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index(int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _apiDetails + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                PersonDetails details = JsonConvert.DeserializeObject<PersonDetails>(jstring);
                return View(details);
            }
            else
                return View(new PersonDetails { PersonId = Id});
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _apiDetails + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Person");
            }
            else
            {
                ModelState.AddModelError("", "There is an API Error");
                return View();
            }
        }

        public IActionResult Add(int Id)
        {
            PersonDetails details = new PersonDetails{ PersonId = Id};
            return View(details);
        }


        [HttpPost]
        public async Task<IActionResult> Add(PersonDetails details)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _apiDetails);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                var content = new StringContent(JsonConvert.SerializeObject(details), Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage message = await _client.SendAsync(request);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Person");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(details);
                }

            }
            else
            {
                return View(details);
            }
        }

        public async Task<IActionResult> Update(int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _apiDetails + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                PersonDetails details = JsonConvert.DeserializeObject<PersonDetails>(jstring);
                return View(details);
            }
            else
                return RedirectToAction("Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(PersonDetails details)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Put, _apiDetails);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                var content = new StringContent(JsonConvert.SerializeObject(details), Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage message = await _client.SendAsync(request);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Person");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(details);
                }

            }
            else
            {
                return View(details);
            }
        }
    }
}
