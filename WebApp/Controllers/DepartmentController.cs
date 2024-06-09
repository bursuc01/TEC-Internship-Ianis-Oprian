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
            var user = HttpContext.Session.GetString("User");

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

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
            var user = HttpContext.Session.GetString("User");

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DepartmentCreation department = new DepartmentCreation();
            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentCreation departmentCreation)
        {
            Department department = new Department();
            department.PositionIds = new List<int>();

            var idList = departmentCreation.PositionIds.Split(',');

            if (idList == null)
            {
                ModelState.AddModelError("Error", "Positions for the department are required!");
                return View(department);
            }

            department.DepartmentName = departmentCreation.DepartmentName;
            
            foreach (var id in idList)
            {
                department.PositionIds.Add(int.Parse(id));
            }

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
            var user = HttpContext.Session.GetString("User");

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var request = new HttpRequestMessage(HttpMethod.Get, _apiDep + Id);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

            HttpResponseMessage message = await _client.SendAsync(request);
            if (message.IsSuccessStatusCode)
            {
                var jstring = await message.Content.ReadAsStringAsync();
                Department department = JsonConvert.DeserializeObject<Department>(jstring);
                var departmetnCreation = new DepartmentCreation();
                
                departmetnCreation.DepartmentId = department.DepartmentId;
                departmetnCreation.DepartmentName = department.DepartmentName;

                var stringBuilder = new StringBuilder();
                foreach (var value in department.PositionIds)
                {
                    stringBuilder.Append(value.ToString() + ',');
                }

                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
                departmetnCreation.PositionIds = stringBuilder.ToString();

                return View(departmetnCreation);
            }
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Update(DepartmentCreation departmentCreation)
        {
            Department department = new Department();
            department.PositionIds = new List<int>();

            var idList = departmentCreation.PositionIds.Split(',');

            if (idList == null)
            {
                ModelState.AddModelError("Error", "Positions for the department are required!");
                return View(department);
            }

            department.DepartmentName = departmentCreation.DepartmentName;
            department.DepartmentId = departmentCreation.DepartmentId;

            foreach (var id in idList)
            {
                department.PositionIds.Add(int.Parse(id));
            }

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
        public IActionResult Back(string controller)
        {
            return RedirectToAction("Index", controller);
        }
    }
}
