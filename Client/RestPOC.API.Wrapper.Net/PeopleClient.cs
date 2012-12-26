namespace RestPOC.API.Wrapper.Net
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using RestPOC.API.Model.Dtos;
    using RestPOC.API.Model.RequestModels;

    public class PeopleClient : IPeopleClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public PeopleClient(HttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException("httpClient");
            }

            if (httpClient.BaseAddress == null)
            {
                throw new ArgumentNullException("httpClient.BaseAddress");
            }

            _httpClient = httpClient;
            _baseUri = httpClient.BaseAddress.ToString().TrimEnd('/').ToLowerInvariant();
        }

        public Task<HttpApiResponseMessage<PaginatedDto<PersonDto>>> GetPeople(int pageIndex, int pageSize)
        {
            // https://localhost:44307/v1/people?pagesize=2&pageindex=1
            var requestUri = string.Format("{0}/v1/people?pagesize={1}&pageindex={2}", _baseUri, pageIndex, pageSize);
            return _httpClient.GetAsync(requestUri).GetHttpApiResponseAsync<PaginatedDto<PersonDto>>();
        }

        public Task<HttpApiResponseMessage<PersonDto>> GetPerson(int personId)
        {
            // https://localhost:44307/v1/people/8
            var requestUri = string.Format("{0}/v1/people/{1}", _baseUri, personId);
            return _httpClient.GetAsync(requestUri).GetHttpApiResponseAsync<PersonDto>();
        }

        public Task<HttpApiResponseMessage<PersonDto>> AddPerson(PersonRequestModel model)
        {
            // https://localhost:44307/v1/people
            var requestUri = string.Format("{0}/v1/people", _baseUri);
            return _httpClient.PostAsJsonAsync(requestUri, model).GetHttpApiResponseAsync<PersonDto>();
        }

        public Task<HttpApiResponseMessage<PersonDto>> UpdatePerson(int personId, PersonRequestModel model)
        {
            // https://localhost:44307/v1/people
            var requestUri = string.Format("{0}/v1/people/{1}", _baseUri, personId);
            return _httpClient.PutAsJsonAsync(requestUri, model).GetHttpApiResponseAsync<PersonDto>();
        }
    }
}