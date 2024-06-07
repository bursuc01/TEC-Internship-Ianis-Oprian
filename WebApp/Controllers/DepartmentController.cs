using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
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
            HttpResponseMessage responseMessage = await _client.GetAsync(_apiDep);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jstring = await responseMessage.Content.ReadAsStringAsync();
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
                var jsondepartment = JsonConvert.SerializeObject(department);
                StringContent content = new StringContent(jsondepartment, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await _client.PostAsync(_apiDep, content);
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
            HttpResponseMessage message = await _client.DeleteAsync(_apiDep + Id);
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
            HttpResponseMessage message = await _client.GetAsync(_apiDep + Id);
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
                var jsondepartment = JsonConvert.SerializeObject(department);
                StringContent content = new StringContent(jsondepartment, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await _client.PutAsync(_apiDep, content);
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
