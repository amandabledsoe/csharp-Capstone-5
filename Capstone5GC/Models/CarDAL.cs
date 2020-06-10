using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Capstone5GC.Models
{
    public class CarDAL
    {
        public HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44328/");
            return client;
        }

        public async Task<List<Car>> GetCars()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("car");
            var cars = await response.Content.ReadAsAsync<List<Car>>();
            return cars;
        }

        public async Task<Car> GetCar(int id)
        {
            var client = GetHttpClient();
            var response = await client.GetAsync($"car/{id}");
            var car = await response.Content.ReadAsAsync<Car>();
            return car;
        }

        public async void DeleteCar(int id)
        {
            var client = GetHttpClient();
            var response = await client.DeleteAsync($"car/{id}");
        }

        public async Task<Car> AddCar(Car car)
        {
            car.Id = 1;
            var client = GetHttpClient();
            var response = await client.PostAsJsonAsync($"/car", car);
            var carResult = await response.Content.ReadAsAsync<Car>();
            return carResult;
        }

        public async void UpdateCar(int id, Car updatedCar)
        {
            var client = GetHttpClient();
            var response = await client.PutAsJsonAsync($"/car/{id}", updatedCar);
        }
    }

}
