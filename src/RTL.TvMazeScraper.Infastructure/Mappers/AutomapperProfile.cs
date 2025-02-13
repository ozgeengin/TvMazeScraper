using AutoMapper;
using RTL.TvMazeScraper.Domain.Models;
using RTL.TvMazeScraper.Infastructure.Data.Entities;
using RTL.TvMazeScraper.Infastructure.Models.Api;

namespace RTL.TvMazeScraper.Infastructure.Mappers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<PersonApiResponseModel, PersonEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TvMazeId, opt => opt.MapFrom(src => src.Id));

            CreateMap<ShowsApiResponseModel, ShowEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TvMazeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Cast, opt => opt.MapFrom(src => src.Cast.Select(x => 
                    new PersonEntity {
                        TvMazeId = x.Person.Id, 
                        Name = x.Person.Name, 
                        Birthday = x.Person.Birthday })));

            CreateMap<PersonEntity, PersonModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TvMazeId));

            CreateMap<ShowEntity, ShowModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TvMazeId));
        }
    }
}
