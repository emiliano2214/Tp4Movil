using AutoMapper;
using BibliotecaLibro.Server.Models;
using BibliotecaLibro.Shared.Models;

namespace BibliotecaLibro.Server.Mapping
{
    public class ProfileLibro : Profile
    {
        public ProfileLibro()
        {
            // Entidad -> DTO
            CreateMap<Libro, LibroDTO>();

            // DTO -> Entidad: valida solo miembros de la fuente (DTO)
            CreateMap<LibroDTO, Libro>(MemberList.Source)
                // si el Id lo genera la DB, descomentá:
                // .ForMember(d => d.IdLibro, o => o.Ignore());
                ;
        }
    }
}
