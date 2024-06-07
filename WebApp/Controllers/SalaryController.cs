using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace WebApp.Controllers
{
    public class SalaryController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _apiSalary;
        private readonly HttpClient _client;

        public SalaryController(IConfiguration config)
        {
            _config = config;
            _apiSalary = _config.GetValue<string>("ApiSettings:ApiUrl") + "Salaries/";
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            List<Salary> list = new List<Salary>();
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.GetAsync(_apiSalary);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Salary>>(jstring);
                return View(list);
            }
            else
                return View(list);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage message = await client.DeleteAsync(_apiSalary + Id);
            if (message.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
                return View();
        }

        public IActionResult Add()
        {
            var salary = new Salary();
            return View(salary);
        }


        [HttpPost]
        public async Task<IActionResult> Add(Salary details)
        {
            if (ModelState.IsValid)
            {
                var jsonDetails = JsonConvert.SerializeObject(details);
                StringContent content = new StringContent(jsonDetails, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await _client.PostAsync(_apiSalary, content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
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
            HttpResponseMessage message = await _client.GetAsync(_apiSalary + Id);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                var details = JsonConvert.DeserializeObject<Salary>(jstring);
                return View(details);
            }
            else
                return RedirectToAction("Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Salary details)
        {
            if (ModelState.IsValid)
            {
                var jsonDetails = JsonConvert.SerializeObject(details);
                StringContent content = new StringContent(jsonDetails, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await _client.PutAsync(_apiSalary, content);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
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
