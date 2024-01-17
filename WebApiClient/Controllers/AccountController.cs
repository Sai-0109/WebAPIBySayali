using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApiClient.Models;

namespace WebApiClient.Controllers
{
    public class AccountController : Controller
    {
        HttpClient _client;
       private readonly string baseAddress;
        public AccountController(IConfiguration configuration)
        {
            _client = new HttpClient();
            baseAddress = configuration["apiAddress"];
           _client.BaseAddress = new Uri(baseAddress);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var response = _client.PostAsJsonAsync("Account", loginModel).Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonData = response.Content.ReadAsStringAsync().Result;
                    UserModel user = JsonSerializer.Deserialize<UserModel>(jsonData);

                    if (user != null)
                    {
                        string accessToken = user.AccessToken;
                        if(accessToken != null)
                        {
                            HttpContext.Session.SetString("accessToken", accessToken);
                        }
                        else
                        {
                            ModelState.AddModelError("accessToken", "Invalid accessToken");
                        }

                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("UserName", "Invalid Username and/or password");
            }

            return View();
        }
    }
}
