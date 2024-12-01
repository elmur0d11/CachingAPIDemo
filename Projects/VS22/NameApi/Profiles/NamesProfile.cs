using AutoMapper;
using NameApi.Dtos;
using NameApi.Models;

namespace NameApi.Profiles
{
    public class NamesProfile : Profile
    {
        public NamesProfile()
        {
            CreateMap<NameCreatedDto, Name>();
            CreateMap<Name, NameReadDto>();
        }
    }
}
