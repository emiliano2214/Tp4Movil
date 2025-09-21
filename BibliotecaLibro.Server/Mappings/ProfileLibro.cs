using AutoMapper;
using BibliotecaLibro.Server.Models;
using BibliotecaLibro.Shared.Models;

public class ProfileLibro : Profile
{
    public ProfileLibro()
    {
        CreateMap<Libro, LibroDTO>(); 
        CreateMap<LibroDTO, Libro>();
    }
}
