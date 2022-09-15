using AutoMapper;
using MagicVilla_VillaApi.Models.DTOs;

namespace MagicVilla_VillaApi.Models.Configurations
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        }
    }
}
