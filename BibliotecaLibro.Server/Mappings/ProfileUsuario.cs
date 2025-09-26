using AutoMapper;
using BibliotecaLibro.Server.Models;
using BibliotecaLibro.Shared.Models;

namespace BibliotecaLibro.Server.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioDTO>();

            // Create -> Entidad (solo valida miembros de la fuente)
            CreateMap<UsuarioCreateDTO, Usuario>(MemberList.Source)
                // .ForMember(d => d.IdUsuario, o => o.Ignore())
                ;

            // Update -> Entidad (NO tocar contraseña)
            CreateMap<UsuarioUpdateDTO, Usuario>(MemberList.Source)
                .ForMember(d => d.Contraseña, o => o.Ignore());
        }
    }
}
