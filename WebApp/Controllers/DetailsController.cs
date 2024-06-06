using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
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
            HttpResponseMessage message = await _client.GetAsync(_apiDetails + Id);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                PersonDetails list = JsonConvert.DeserializeObject<PersonDetails>(jstring);
                return View(list);
            }
            else
                return View(new List<PersonDetails>());
        }

        public async Task<IActionResult> Delete(int Id)
        {
            HttpResponseMessage message = await _client.DeleteAsync(_apiDetails + Id);
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
    }
}
