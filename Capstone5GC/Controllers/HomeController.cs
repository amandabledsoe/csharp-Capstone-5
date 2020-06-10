using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Capstone5GC.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace Capstone5GC.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarDAL carDal = new CarDAL();

        public async Task<IActionResult> Index()
        {
            List<Car> cars = await carDal.GetCars();
            return View(cars);
        }

        public async Task<IActionResult> Search(int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");

            var response = await client.GetAsync($"car/{id}.json");

            var result = await response.Content.ReadAsStringAsync();

            Car car = JsonConvert.DeserializeObject<Car>(result);

            return View(car);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
