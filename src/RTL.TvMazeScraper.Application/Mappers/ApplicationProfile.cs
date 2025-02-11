using AutoMapper;
using RTL.TvMazeScraper.Application.Models;
using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Application.Mappers
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CastPersonEntity, CastPersonDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Person.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Person.Birthday));

            CreateMap<CastPersonDto, CastPersonEntity>()
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => new PersonEntity
                {
                    Id = src.Id,
                    Name = src.Name,
                    Birthday = src.Birthday
                }))
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Show, opt => opt.Ignore())
                .ForMember(dest => dest.ShowId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ShowEntity, ShowDto>().ReverseMap();
        }
    }
}
