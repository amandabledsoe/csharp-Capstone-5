using Capstone5GC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone5GC.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarDealershipDbContext _context;

        public CarController(CarDealershipDbContext context)
        {
            _context = context;
        }

        //GET: api/Car
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            var cars = await _context.Car.ToListAsync();
            return cars;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if(car == null)
            {
                return NotFound();
            }
            return car;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
                //returns 204 status code -- successful and the AP is not returning any content
            }
            else
            {
                _context.Car.Remove(car);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        //POST: api/car
        [HttpPost]
        public async Task<ActionResult<Car>> AddCar(Car newCar)
        {
            if (ModelState.IsValid)
            {
                _context.Car.Add(newCar);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(newCar), new { id = newCar.Id }, newCar);
            }
            else
            {
                return BadRequest();
            }
        }

        //PUT: api/car/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Car>> UpdateCar(int id, Car updatedCar)
        {
            if (id != updatedCar.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(updatedCar).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Accepted($"https://localhost:44328/api/Car/{id}", updatedCar);
            }
        }
    }
}
