using AutoMapper;
using CitiManagerApi.Dtos;
using CitiManagerApi.Entities;

namespace CitiManagerApi.Mappers
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<City,CityForListDto>()
                .ForMember(dest => dest.PhotoUrl, option =>
                {
                    option.MapFrom(src => src.CityImages.FirstOrDefault(p=>p.IsMain).Url);
                });

            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
