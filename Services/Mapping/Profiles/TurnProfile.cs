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
    public class TurnProfile : Profile
    {
        public TurnProfile() 
        {
            CreateMap<Turns, TurnsDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<List<Turns>, List<TurnsDTO>>()
                .ConvertUsing(src => src.Select(e => new TurnsDTO { Id = e.Id, IdPitch = e.IdPitch, IdUser = e.IdUser, Dia = (DateTime)e.Dia, Descripcion = e.Descripcion }).ToList());

            CreateMap<TurnsDTO, Turns>();

            CreateMap<TurnsDTO, Turns>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
