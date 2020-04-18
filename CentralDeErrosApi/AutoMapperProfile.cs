using AutoMapper;
using CentralDeErrosApi.DTO;
using CentralDeErrosApi.Models;

namespace CentralDeErrosApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Environment, EnvironmentDTO>().ReverseMap();
            CreateMap<LogErrorOccurrence, LogErrorOccurrenceDTO>().ReverseMap();
            CreateMap<Level, LevelDTO>().ReverseMap();
            CreateMap<Situation, SituationDTO>().ReverseMap();
        }
    }
}
