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
    public class PersonController : Controller
    {

        private readonly IConfiguration _config;
        private readonly string _apiPerson;
        private readonly HttpClient _client;

        public PersonController(IConfiguration config)
        {
            _config = config;
            _apiPerson = _config.GetValue<string>("ApiSettings:ApiUrl") + "Persons/";
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _apiPerson);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                List<PersonInformation> list = JsonConvert.DeserializeObject<List<PersonInformation>>(jstring);
                return View(list);
            }
            else
                return View(new List<PersonInformation>());
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _apiPerson + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "There is an API Error");
                return View();
            }
        }

        public IActionResult Add()
        {
            Person person = new Person();
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _apiPerson);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage message = await _client.SendAsync(request);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(person);
                }

            }
            else
            {
                return View(person);
            }
        }

        public async Task<IActionResult> Update(int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _apiPerson + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                Person person = JsonConvert.DeserializeObject<Person>(jstring);
                return View(person);
            }
            else
                return RedirectToAction("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Person person)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Put, _apiPerson);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                var content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage message = await _client.SendAsync(request);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(person);
                }
            }
            else
                return View(person);
        }


    }
}
