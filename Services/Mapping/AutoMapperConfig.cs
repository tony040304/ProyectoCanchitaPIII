using AutoMapper;
using Models.DTO;
using Models.MODELS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Mapping.Profiles;

namespace Services.Mapping
{
    public static class AutoMapperConfig
    {
        public static IMapper Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PitchProfile>();
                cfg.AddProfile<TurnProfile>();
                cfg.AddProfile<UserProfile>();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            return mapper;
        }
    }
}
