namespace RestPOC.API.Wrapper.Net
{
    using System.Threading.Tasks;
    using RestPOC.API.Model.Dtos;
    using RestPOC.API.Model.RequestModels;

    public interface IPeopleClient
    {
        Task<HttpApiResponseMessage<PaginatedDto<PersonDto>>> GetPeople(int pageIndex, int pageSize);
        Task<HttpApiResponseMessage<PersonDto>> GetPerson(int personId);

        Task<HttpApiResponseMessage<PersonDto>> AddPerson(PersonRequestModel model);
        Task<HttpApiResponseMessage<PersonDto>> UpdatePerson(int personId, PersonRequestModel model);
    }
}