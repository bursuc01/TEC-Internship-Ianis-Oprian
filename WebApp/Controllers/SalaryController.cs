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
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

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
            var request = new HttpRequestMessage(HttpMethod.Get, _apiSalary);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
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
            var request = new HttpRequestMessage(HttpMethod.Delete, _apiSalary + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
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
        public async Task<IActionResult> Add(Salary salary)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _apiSalary);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                var content = new StringContent(JsonConvert.SerializeObject(salary), Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage message = await _client.SendAsync(request);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(salary);
                }

            }
            else
            {
                return View(salary);
            }
        }

        public async Task<IActionResult> Update(int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _apiSalary + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
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
        public async Task<IActionResult> Update(Salary salary)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Put, _apiSalary);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                var content = new StringContent(JsonConvert.SerializeObject(salary), Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage message = await _client.SendAsync(request);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return View(salary);
                }

            }
            else
            {
                return View(salary);
            }
        }
    }
}
