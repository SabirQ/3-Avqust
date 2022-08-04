using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.DAL;
using Task.DTOs.Cars;
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
        public IActionResult GetAll(int page=1,string search=null)
        {
            var query = _context.Cars.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Brand.Contains(search));
            }
            CarListDto carListDto = new CarListDto
            {
                CarListItemDtos=query.Select(c=>new CarListItemDto { Brand=c.Brand,Model=c.Model,Price=c.Price,Color=c.Color})
                .Skip((page-1)*4).Take(4).ToList(),
                TotalCount=query.Where(c=>c.Display==true).Count()
            };
            return Ok(carListDto);
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Car car = _context.Cars.FirstOrDefault(c=>c.Id==id);
            if (car == null) return StatusCode(StatusCodes.Status404NotFound);
            CarGetDto dto = new CarGetDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Price = car.Price,
                Color = car.Color,
                Display = car.Display
            };
            return Ok(dto);
        }
        [HttpPost("create")]
        public async  Task<IActionResult> Create(CarPostDto carDto)
        {
            if (carDto==null) return StatusCode(StatusCodes.Status404NotFound);
            Car car = new Car
            {
                Brand = carDto.Brand,
                Model = carDto.Model,
                Price = carDto.Price,
                Color = carDto.Color,
                Display = carDto.Display
            };
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return StatusCode(201, new{ Id=car.Id, car=carDto });
        }
        [HttpPut("update/{id}")]
        public IActionResult Update(int id,CarPostDto car)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Car existed=_context.Cars.FirstOrDefault(c=>c.Id==id);
            if (existed==null) return StatusCode(StatusCodes.Status404NotFound);
            _context.Entry(existed).CurrentValues.SetValues(car);
            _context.SaveChanges();
            return Ok(car);
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
