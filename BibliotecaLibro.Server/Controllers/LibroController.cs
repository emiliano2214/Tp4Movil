using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaLibro.Server.Models;
using BibliotecaLibro.Server.Data;
using BibliotecaLibro.Shared.Models;

namespace BibliotecaLibro.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly BibliotecaDbContext _context;
        private readonly IMapper _mapper;

        public LibroController(BibliotecaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Libro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDTO>>> GetLibro()
        {
            var libros = await _context.Libro.ToListAsync();
            return _mapper.Map<List<LibroDTO>>(libros); // ENTIDAD → DTO
        }

        // GET: api/Libro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDTO>> GetLibro(int id)
        {
            var libro = await _context.Libro.FindAsync(id);

            if (libro == null)
                return NotFound();

            return _mapper.Map<LibroDTO>(libro); // ENTIDAD → DTO
        }

        // POST: api/Libro
        [HttpPost]
        public async Task<ActionResult<LibroDTO>> PostLibro(LibroDTO libroDTO)
        {
            var libro = _mapper.Map<Libro>(libroDTO); // DTO → ENTIDAD
            _context.Libro.Add(libro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLibro), new { id = libro.IdLibro },
                _mapper.Map<LibroDTO>(libro)); // ENTIDAD → DTO
        }

        // PUT: api/Libro/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro(int id, LibroDTO libroDTO)
        {
            if (id != libroDTO.IdLibro)
                return BadRequest();

            var libro = _mapper.Map<Libro>(libroDTO); // DTO → ENTIDAD
            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Libro.Any(e => e.IdLibro == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Libro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _context.Libro.FindAsync(id);
            if (libro == null)
                return NotFound();

            _context.Libro.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
