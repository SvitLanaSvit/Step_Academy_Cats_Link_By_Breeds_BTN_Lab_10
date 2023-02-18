using ASP_Meeting_8.Data.Entities;
using ASP_Meeting_8.Models.DTO;
using AutoMapper;
namespace ASP_Meeting_8.AutoMapperProfiles
{
    public class CatProfile : Profile
    {
        public CatProfile()
        {
            CreateMap<Cat, CatDTO>().ReverseMap();
        }
    }
}
