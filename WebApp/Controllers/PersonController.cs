using WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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
            HttpResponseMessage message = await _client.GetAsync(_apiPerson);
            if(message.IsSuccessStatusCode)
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
            HttpResponseMessage message = await _client.DeleteAsync(_apiPerson + Id);
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
            if(ModelState.IsValid)
            {
                var jsonPerson = JsonConvert.SerializeObject(person);
                StringContent content = new StringContent(jsonPerson,Encoding.UTF8,"application/json");
                HttpResponseMessage message = await _client.PostAsync(_apiPerson, content);
                if(message.IsSuccessStatusCode)
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
            HttpResponseMessage message = await _client.GetAsync(_apiPerson + Id);
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
                var jsonperson = JsonConvert.SerializeObject(person);
                StringContent content = new StringContent(jsonperson, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await _client.PutAsync(_apiPerson, content);
                if(message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(person);
                }
            }
            else
                return View(person);
        }
     

    }
}
