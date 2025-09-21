using AutoMapper;
using BibliotecaLibro.Server.Models;
using BibliotecaLibro.Shared.Models;

namespace BibliotecaLibro.Server.Mappings
{
    public class ProfileUsuario : Profile
    {
        public ProfileUsuario()
        {
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();
        }
    }
}