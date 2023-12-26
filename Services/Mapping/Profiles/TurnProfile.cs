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
                .ForMember(dest => dest.IdTurns, opt => opt.MapFrom(src => src.IdTurns));

            CreateMap<List<Turns>, List<TurnsDTO>>()
                .ConvertUsing(src => src.Select(e => new TurnsDTO { IdTurns = e.IdTurns, IdPitch = e.IdPitch, IdUsers = e.IdUsers, Dias = (DateTime)e.Dias }).ToList());

            CreateMap<TurnsDTO, Turns>();

            CreateMap<TurnsDTO, Turns>()
                .ForMember(dest => dest.IdTurns, opt => opt.MapFrom(src => src.IdTurns));
        }
    }
}
