using AutoMapper;
using Models.DTO;
using Models.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping.Profiles
{
    public class PitchProfile : Profile
    {
        public PitchProfile()
        {
            CreateMap<Pitch, PitchDTO>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<List<Pitch>, List<PitchDTO>>()
                .ConvertUsing(src => src.Select(x => new PitchDTO { Id = x.Id, Nombre = x.Nombre, Password = x.Password, Email = x.Email, Horario = x.Horario, Ubicacion = x.Ubicacion, IsBlocked = x.IsBlocked }).ToList());

            CreateMap<PitchDTO, Pitch>();

            CreateMap<PitchDTO, Pitch>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));
        }
    }
}
