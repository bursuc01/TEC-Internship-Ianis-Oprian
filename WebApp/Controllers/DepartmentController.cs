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
    public class DepartmentController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _apiDep;
        private readonly HttpClient _client;

        public DepartmentController(IConfiguration config)
        {
            _config = config;
            _apiDep = _config.GetValue<string>("ApiSettings:ApiUrl") + "Departments/";
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index()
        {
            List<Department> list = new List<Department>();
            var request = new HttpRequestMessage(HttpMethod.Get, _apiDep);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);

            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<Department>>(jstring);
                return View(list);
            }
            else
                return View(list);
        }
        public IActionResult Add()
        {
            Department department = new Department();
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            department.PositionIds = new List<int>();
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, _apiDep);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                var content = new StringContent(JsonConvert.SerializeObject(department), Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage message = await _client.SendAsync(request);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Error", "There is an API error");
                    return View(department);

                }
            }
            else
            {
                return View(department);
            }
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _apiDep + Id);
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

        public async Task<IActionResult> Update(int Id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _apiDep + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                Department department = JsonConvert.DeserializeObject<Department>(jstring);
                return View(department);
            }
            else
                return RedirectToAction("Add");
        }
        [HttpPost]
        public async Task<IActionResult> Update(Department department)
        {
            department.PositionIds = new List<int>();

            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Put, _apiDep);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                var content = new StringContent(JsonConvert.SerializeObject(department), Encoding.UTF8, "application/json");
                request.Content = content;

                HttpResponseMessage message = await _client.SendAsync(request);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    return View(department);
            }
            else
                return View(department);
        }

    }
}
