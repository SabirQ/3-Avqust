using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Task.DAL;
using Task.DTOs.Engines;
using Task.Models;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnginesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnginesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0) return StatusCode(StatusCodes.Status404NotFound);
            Engine engine = await _context.Engines.FirstOrDefaultAsync(e => e.Id == id);
            if (engine==null) return StatusCode(StatusCodes.Status404NotFound);
            EngineGetDto dto = new EngineGetDto
            {
                Id = engine.Id,
                Name = engine.Name,
                Value = engine.Value,
                HP = engine.HP,
                Torque = engine.Torque
            };
            return Ok(dto);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(EnginePostDto dto)
        {
            if (dto == null) return StatusCode(StatusCodes.Status404NotFound);
            Engine engine = new Engine
            {
                Name = dto.Name,
                Value = dto.Value,
                HP = dto.HP,
                Torque = dto.Torque
            };
           await _context.Engines.AddAsync(engine);
           await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
