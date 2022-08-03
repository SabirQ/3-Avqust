using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Task.DAL;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Car> cars = _context.Cars.Where(c=>c.Display==true).ToList();
                return Ok(cars);
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Car car = _context.Cars.FirstOrDefault(c=>c.Id==id);
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(car);
        }
        [HttpPost("create")]
        public IActionResult Create(Car car)
        {
            if (car==null) return StatusCode(StatusCodes.Status404NotFound);
             _context.Cars.Add(car);
            _context.SaveChanges();
            return StatusCode(201,car);
        }
        [HttpPut("update/{id}")]
        public IActionResult Update(int id,Car car)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Car existed=_context.Cars.FirstOrDefault(c=>c.Id==id);
            if (existed==null) return StatusCode(StatusCodes.Status404NotFound);

            existed.Brand = car.Brand;
            existed.Model = car.Model;
            existed.Price = car.Price;
            existed.Color=car.Color;
            existed.Display=car.Display;
            _context.SaveChanges();
            return Ok(existed);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Car car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car == null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Remove(car);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("change/{id}")]
        public IActionResult ChangeDisplay(int id,bool display)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Car existed = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (existed == null) return StatusCode(StatusCodes.Status404NotFound);

            existed.Display = display;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
