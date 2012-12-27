namespace RestPOC.API.Wrapper.Net
{
    using System.Threading.Tasks;
    using RestPOC.API.Model.Dtos;
    using RestPOC.API.Model.RequestModels;

    public interface IPeopleClient
    {
        Task<HttpApiResponseMessage<PaginatedDto<PersonDto>>> GetPeopleAsync(int pageIndex, int pageSize);
        Task<HttpApiResponseMessage<PersonDto>> GetPersonAsync(int personId);

        Task<HttpApiResponseMessage<PersonDto>> AddPersonAsync(PersonRequestModel model);
        Task<HttpApiResponseMessage<PersonDto>> UpdatePersonAsync(int personId, PersonRequestModel model);
    }
}