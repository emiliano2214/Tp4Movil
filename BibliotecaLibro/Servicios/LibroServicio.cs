using BibliotecaLibro.Shared.Models; // Modelos en MAUI (solo propiedades)
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BibliotecaLibro.Servicios
{
    public class LibroServicio
    {
        private readonly HttpClient _http;

        public LibroServicio(HttpClient http)
        {
            _http = http;
        }

        // Obtener todos los libros
        public async Task<List<LibroDTO>> GetLibrosAsync()
        {
            try
            {
                var libros = await _http.GetFromJsonAsync<List<LibroDTO>>("Libro");
                return libros ?? new List<LibroDTO>();
            }
            catch
            {
                // Retorna lista vacía en caso de error
                return new List<LibroDTO>();
            }
        }

        // Obtener un libro por id
        public async Task<LibroDTO> GetLibroAsync(int id)
        {
            try
            {
                return await _http.GetFromJsonAsync<LibroDTO>($"Libro/{id}");
            }
            catch
            {
                return null;
            }
        }

        // Crear un libro nuevo
        public async Task<bool> AddLibroAsync(LibroDTO libroDTO)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("Libro", libroDTO);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Actualizar un libro existente
        public async Task<bool> UpdateLibroAsync(LibroDTO libroDTO)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"Libro/{libroDTO.IdLibro}", libroDTO);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Eliminar un libro
        public async Task<bool> DeleteLibroAsync(int id)
        {
            try
            {
                var response = await _http.DeleteAsync($"Libro/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
