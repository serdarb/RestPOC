using AutoMapper;
using RestPOC.API.Model.Dtos;
using RestPOC.API.Model.RequestModels;
using RestPOC.Domain;

using System.Linq;

namespace RestPOC.API.Config {
    
    public static class AutoMapperConfig {

        public static void Configure() {

            Mapper.CreateMap<Person, PersonDto>();
            Mapper.CreateMap<PaginatedList<Person>, PaginatedDto<PersonDto>>()
                .ForMember(dest => dest.Items,
                           opt => opt.MapFrom(
                               src => src.Select(
                                   entity => Mapper.Map<Person, PersonDto>(entity))));

            Mapper.CreateMap<PersonRequestModel, Person>();
        }
    }
}