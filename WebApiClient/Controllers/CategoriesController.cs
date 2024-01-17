using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using WebApiClient.Models;

namespace WebApiClient.Controllers
{
    public class CategoriesController : Controller
    {
        HttpClient client;
        string baseAddress;
        public CategoriesController(IConfiguration configuration)
        {
            this.client = new HttpClient();
            baseAddress= configuration["apiAddress"];
            this.client.BaseAddress = new Uri(baseAddress);
        }
        public IActionResult Index()
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:5063/api/");



            //string result =
            //     client.GetStringAsync(client.BaseAddress + "Categories/?type"+"true").Result;
            string accessToken = HttpContext.Session.GetString("accessToken");

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
            string result = client.GetStringAsync(baseAddress + "Categories").Result;

            List <CategoryModel> categories =
                JsonSerializer.Deserialize<List<CategoryModel>>(result);

            return View(categories);
        }
        public IActionResult Details(int id)
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:5063/api/");

            string result =
                 client.GetStringAsync(client.BaseAddress + $"Categories/{id}").Result;

            CategoryModel category = JsonSerializer.Deserialize<CategoryModel>(result);

            return View(category);
        }
    }
}