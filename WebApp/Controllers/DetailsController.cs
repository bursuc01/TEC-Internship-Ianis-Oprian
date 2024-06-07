using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
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
            HttpResponseMessage message = await _client.GetAsync(_apiDetails + Id);
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
                var jsonDetails = JsonConvert.SerializeObject(details);
                StringContent content = new StringContent(jsonDetails, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await _client.PostAsync(_apiDetails, content);
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
            HttpResponseMessage message = await _client.GetAsync(_apiDetails + Id);
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
                var jsonDetails = JsonConvert.SerializeObject(details);
                StringContent content = new StringContent(jsonDetails, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await _client.PutAsync(_apiDetails, content);
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
