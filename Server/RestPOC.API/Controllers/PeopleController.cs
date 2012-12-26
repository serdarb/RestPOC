using AutoMapper;
using RestPOC.API.Model.Dtos;
using RestPOC.API.Model.RequestCommands;
using RestPOC.API.Model.RequestModels;
using RestPOC.Domain;
using RestPOC.Service;

using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestPOC.API.Controllers
{
    public class PeopleController : ApiController
    {
        private readonly IPeopleService peopleService;
        public PeopleController(IPeopleService peopleService)
        {
            this.peopleService = peopleService;
        }

        [Authorize(Users = "superuser")]
        public PaginatedDto<PersonDto> GetPeople(PaginatedRequestCommand cmd)
        {
            var peopleResult = this.peopleService.GetAll(cmd.PageIndex, cmd.PageSize);
            if (!peopleResult.IsOK)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return Mapper.Map<PaginatedList<Person>, PaginatedDto<PersonDto>>(peopleResult.Result);
        }

        public PersonDto GetPerson(int id)
        {
            var personResult = this.peopleService.GetById(id);

            if (!personResult.IsOK || personResult.Result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Person, PersonDto>(personResult.Result);
        }

        [Authorize]
        public HttpResponseMessage PostPerson(PersonRequestModel requestModel)
        {
            var person = Mapper.Map<PersonRequestModel, Person>(requestModel);
            var personResult = this.peopleService.AddNewUser(person);
            if (!personResult.IsOK)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            var personDto = Mapper.Map<Person, PersonDto>(personResult.Result);
            return Request.CreateResponse(HttpStatusCode.Created, personDto);
        }

        [Authorize]
        public PersonDto PutPerson(int id, PersonRequestModel requestModel) {
            
            var person = Mapper.Map<PersonRequestModel, Person>(requestModel);
            person.Id = id;
            var personResult = this.peopleService.UpdateUser(person);
            if (!personResult.IsOK)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return Mapper.Map<Person, PersonDto>(personResult.Result);
        }
    }
}