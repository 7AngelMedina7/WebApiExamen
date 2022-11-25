using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Entidades;

namespace WebApplication1.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GetDepartamentoDTO, Departamento>();
            CreateMap<Departamento, GetDepartamentoDTO>();
            CreateMap<Departamento, DepasInquilinoDTO>()
                .ForMember(depaDTO => depaDTO.Inquilinos, opciones => opciones.MapFrom(MapDepaInquilino));
        }
        private List<InquilinoDTO> MapDepaInquilino(Departamento depa, GetDepartamentoDTO getDepartamentoDTO)
        {
            var result = new List<InquilinoDTO>();

            if (depa.Inquilino == null) { return result; }

            foreach (var inquilino in depa.Inquilino)
            {
                result.Add(new InquilinoDTO()
                {
                    Id = inquilino.id,
                    Nombre = inquilino.Nombre
                });
            }

            return result;
        }
       
    }
}
