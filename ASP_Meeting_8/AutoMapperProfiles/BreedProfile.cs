using ASP_Meeting_8.Data.Entities;
using ASP_Meeting_8.Models.DTO;
using AutoMapper;

namespace ASP_Meeting_8.AutoMapperProfiles
{
    public class BreedProfile : Profile
    {
        public BreedProfile()
        {
            CreateMap<Breed, BreedDTO>().ReverseMap();
        }
    }
}
