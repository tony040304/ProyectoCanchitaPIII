using AutoMapper;
using Models.DTO;
using Models.MODELS;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Users, UserDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));

            CreateMap<List<Users>, List<UserDTO>>()
                .ConvertUsing(src => src.Select(e => new UserDTO { Username = e.Username, Id = e.Id, Email = e.Email, Userpassword = e.Userpassword }).ToList());

            CreateMap<UserDTO, Users>();

            CreateMap<UserDTO, Users>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));
        }
    }
}
