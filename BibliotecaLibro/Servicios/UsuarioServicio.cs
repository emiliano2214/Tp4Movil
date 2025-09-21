using BibliotecaLibro.Shared.Models; // Modelos en MAUI (solo propiedades)
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BibliotecaLibro.Servicios
{
    public class UsuarioServicio
    {
        private readonly HttpClient _http;

        public UsuarioServicio(HttpClient http)
        {
            _http = http;
        }

        // Obtener todos los usuarios
        public async Task<List<UsuarioDTO>> GetUsuariosAsync()
        {
            try
            {
                var usuariosDTO = await _http.GetFromJsonAsync<List<UsuarioDTO>>("Usuario");
                return usuariosDTO ?? new List<UsuarioDTO>();
            }
            catch
            {
                return new List<UsuarioDTO>();
            }
        }

        // Obtener un usuario por id
        public async Task<UsuarioDTO> GetUsuarioAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<UsuarioDTO>($"Usuario/{id}");
            }
            catch
            {
                return null;
            }
        }

        // Agregar un usuario
        public async Task<bool> AddUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("Usuario", usuarioDTO);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Actualizar un usuario
        public async Task<bool> UpdateUsuarioAsync(UsuarioDTO usuarioDTO)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"Usuario/{usuarioDTO.IdUsuario}", usuarioDTO);
                return response.IsSuccessStatusCode;
            }
            catch  
            {
                return false;
            }
        }

        // Eliminar un usuario
        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"Usuario/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
