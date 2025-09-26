// BibliotecaLibro.Server/Controllers/UsuarioController.cs
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaLibro.Server.Data;
using BibliotecaLibro.Server.Models;
using BibliotecaLibro.Shared.Models;

namespace BibliotecaLibro.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly BibliotecaDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioController(BibliotecaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST: api/Usuario/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UsuarioDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var usuario = await _context.Usuario
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u =>
                        u.NombreUsuario == loginDTO.NombreUsuario &&
                        u.Contraseña == loginDTO.Contraseña);

                if (usuario == null)
                    return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });

                var dto = _mapper.Map<UsuarioDTO>(usuario);
                // Por seguridad, no devolver contraseña al front
                dto.Contraseña = string.Empty;
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al iniciar sesión", detalle = ex.Message });
            }
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var entidades = await _context.Usuario.AsNoTracking().ToListAsync();
            var lista = _mapper.Map<List<UsuarioDTO>>(entidades);

            // Limpiar contraseña en la salida
            foreach (var u in lista) u.Contraseña = string.Empty;

            return Ok(lista);
        }

        // GET: api/Usuario/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var entity = await _context.Usuario.AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdUsuario == id);

            if (entity == null) return NotFound();

            var dto = _mapper.Map<UsuarioDTO>(entity);
            dto.Contraseña = string.Empty; // no exponer
            return Ok(dto);
        }

        // POST: api/Usuario  (CREATE con UsuarioCreateDTO)
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario([FromBody] UsuarioCreateDTO createDTO)
        {
            var entity = _mapper.Map<Usuario>(createDTO);
            _context.Usuario.Add(entity);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<UsuarioDTO>(entity);
            dto.Contraseña = string.Empty; // no exponer
            return CreatedAtAction(nameof(GetUsuario), new { id = entity.IdUsuario }, dto);
        }

        // PUT: api/Usuario/5  (UPDATE con UsuarioUpdateDTO, sin contraseña)
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] UsuarioUpdateDTO updateDTO)
        {
            if (id != updateDTO.IdUsuario) return BadRequest();

            var entity = await _context.Usuario.FindAsync(id);
            if (entity == null) return NotFound();

            _mapper.Map(updateDTO, entity); // No modifica contraseña (ignorada en el perfil)
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var entity = await _context.Usuario.FindAsync(id);
            if (entity == null) return NotFound();

            _context.Usuario.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
