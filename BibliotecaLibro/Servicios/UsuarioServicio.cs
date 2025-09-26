// BibliotecaLibro.Client/Servicios/UsuarioServicio.cs
using BibliotecaLibro.Shared.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace BibliotecaLibro.Servicios
{
    public class UsuarioServicio
    {
        private readonly HttpClient _http;
        private static readonly JsonSerializerOptions _json = new()
        { PropertyNameCaseInsensitive = true };

        // Si BaseAddress termina en /api/ => BASE = ""
        // Si BaseAddress es la raíz           => BASE = "api/"
        private const string BASE = ""; // "api/";
        private static string R(string path) => $"{BASE}{path}".TrimStart('/');

        public UsuarioServicio(HttpClient http) => _http = http;

        // LOGIN
        public async Task<UsuarioDTO?> LoginAsync(LoginDTO loginDTO, CancellationToken ct = default)
        {
            try
            {
                var res = await _http.PostAsJsonAsync(R("Usuario/Login"), loginDTO, _json, ct);
                if (res.IsSuccessStatusCode)
                    return await res.Content.ReadFromJsonAsync<UsuarioDTO>(_json, ct);

                if (res.StatusCode == HttpStatusCode.Unauthorized)
                    return null;

                // (opcional) log interno de error:
                var body = await res.Content.ReadAsStringAsync(ct);
                System.Diagnostics.Debug.WriteLine($"Login error {res.StatusCode}: {body}");
                return null;
            }
            catch { return null; }
        }

        // GET ALL
        public async Task<List<UsuarioDTO>> GetUsuariosAsync(CancellationToken ct = default)
        {
            try
            {
                var lista = await _http.GetFromJsonAsync<List<UsuarioDTO>>(R("Usuario"), _json, ct);
                return lista ?? new List<UsuarioDTO>();
            }
            catch { return new List<UsuarioDTO>(); }
        }

        // GET BY ID
        public async Task<UsuarioDTO?> GetUsuarioAsync(int id, CancellationToken ct = default)
        {
            try
            {
                return await _http.GetFromJsonAsync<UsuarioDTO>(R($"Usuario/{id}"), _json, ct);
            }
            catch { return null; }
        }

        // CREATE (UsuarioCreateDTO)
        public async Task<bool> AddUsuarioAsync(UsuarioCreateDTO dto, CancellationToken ct = default)
        {
            try
            {
                var res = await _http.PostAsJsonAsync(R("Usuario"), dto, _json, ct);
                return res.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // UPDATE (UsuarioUpdateDTO)
        public async Task<bool> UpdateUsuarioAsync(UsuarioUpdateDTO dto, CancellationToken ct = default)
        {
            try
            {
                var res = await _http.PutAsJsonAsync(R($"Usuario/{dto.IdUsuario}"), dto, _json, ct);
                return res.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        // DELETE
        public async Task<bool> DeleteUsuarioAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var res = await _http.DeleteAsync(R($"Usuario/{id}"), ct);
                return res.IsSuccessStatusCode;
            }
            catch { return false; }
        }
    }
}
