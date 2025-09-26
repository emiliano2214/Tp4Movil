using BibliotecaLibro.Shared.Models;
using BibliotecaLibro.Shared.Json; // ConversorJsonFechaOnly
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BibliotecaLibro.Servicios
{
    public class LibroServicio
    {
        private readonly HttpClient _http;

        // Opciones JSON compartidas (incluye conversor para DateOnly)
        private static readonly JsonSerializerOptions _json = new()
        {
            PropertyNameCaseInsensitive = true
        };

        static LibroServicio()
        {
            _json.Converters.Add(new ConversorJsonFechaOnly());
        }

        public LibroServicio(HttpClient http)
        {
            _http = http;
        }

        // Obtener todos los libros
        public async Task<List<LibroDTO>> GetLibrosAsync(CancellationToken ct = default)
        {
            try
            {
                var libros = await _http.GetFromJsonAsync<List<LibroDTO>>("Libro", _json, ct);
                return libros ?? new List<LibroDTO>();
            }
            catch
            {
                return new List<LibroDTO>();
            }
        }

        // Obtener un libro por id
        public async Task<LibroDTO?> GetLibroAsync(int id, CancellationToken ct = default)
        {
            try
            {
                return await _http.GetFromJsonAsync<LibroDTO>($"Libro/{id}", _json, ct);
            }
            catch
            {
                return null;
            }
        }

        // Crear un libro nuevo

        public async Task<bool> AddLibroAsync(LibroDTO dto, CancellationToken ct = default)
        {
            try
            {
                var res = await _http.PostAsJsonAsync("Libro", dto, _json, ct);
                var body = await res.Content.ReadAsStringAsync(ct);
                System.Diagnostics.Debug.WriteLine(
                    $"POST /Libro -> {(int)res.StatusCode} {res.StatusCode}\n{body}");
                return res.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR AddLibroAsync: " + ex);
                return false;
            }
        }

        // Actualizar un libro existente
        public async Task<bool> UpdateLibroAsync(LibroDTO libroDTO, CancellationToken ct = default)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"Libro/{libroDTO.IdLibro}", libroDTO, _json, ct);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Eliminar un libro
        public async Task<bool> DeleteLibroAsync(int id, CancellationToken ct = default)
        {
            try
            {
                var response = await _http.DeleteAsync($"Libro/{id}", ct);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
