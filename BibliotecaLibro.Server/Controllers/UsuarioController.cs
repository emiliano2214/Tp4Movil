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
                    .FirstOrDefaultAsync(u => u.NombreUsuario == loginDTO.NombreUsuario
                                           && u.Contraseña == loginDTO.Contraseña);

                if (usuario == null)
                    return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });

                // 🔑 Acá el mapeo correcto: Usuario -> UsuarioDTO
                var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

                // Seguridad: nunca mandar contraseña al cliente
                usuarioDTO.Contraseña = null;

                return Ok(usuarioDTO);
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
            var usuarios = await _context.Usuario.ToListAsync();
            return _mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
                return NotFound();

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario },
                _mapper.Map<UsuarioDTO>(usuario));
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDTO usuarioDTO)
        {
            if (id != usuarioDTO.IdUsuario)
                return BadRequest();

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            // Mapeo DTO -> Entidad (actualiza solo los campos que tenga el DTO)
            _mapper.Map(usuarioDTO, usuario);

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                return NotFound();

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
