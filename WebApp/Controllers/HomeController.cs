using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly string _apiAuth;
        private readonly HttpClient _client;

        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _apiAuth = _config.GetValue<string>("ApiSettings:ApiUrl") + "Authenticate/";
            _client = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Home(User User)
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                var user = HttpContext.Session.GetString("User");
                var isAdmin = HttpContext.Session.GetString("IsAdmin") == "True";
                return View(new User { Name = user, IsAdmin = isAdmin });
            }

            if (ModelState.IsValid)
            {
                var jsonDetails = JsonConvert.SerializeObject(User);
                StringContent content = new StringContent(jsonDetails, Encoding.UTF8, "application/json");
                HttpResponseMessage message = await _client.PostAsync(_apiAuth, content);
                if (message.IsSuccessStatusCode)
                {
                    var jstring = await message.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<Auth>(jstring);
                    HttpContext.Session.SetString("Token", token.Token);

                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token.Token);

                    var isAdminClaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "IsAdmin");
                    User.IsAdmin = isAdminClaim.Value == "True";

                    HttpContext.Session.SetString("User", User.Name);
                    HttpContext.Session.SetString("IsAdmin", isAdminClaim.Value);

                    return View(User);
                }
                else
                {
                    ModelState.AddModelError("", "There is an API Error");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Username or password incorrect");
                return RedirectToAction("Index");
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("User");

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
