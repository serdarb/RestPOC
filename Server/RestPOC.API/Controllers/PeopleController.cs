using AutoMapper;
using RestPOC.API.Model.Dtos;
using RestPOC.API.Model.RequestCommands;
using RestPOC.API.Model.RequestModels;
using RestPOC.Domain;
using RestPOC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestPOC.API.Controllers {
   
    public class PeopleController : ApiController {

        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService) {

            _peopleService = peopleService;
        }

        public PaginatedDto<PersonDto> GetPeople(PaginatedRequestCommand cmd) {

            OperationResult<PaginatedList<Person>> peopleResult = _peopleService.GetAll(cmd.Page, cmd.Take);
            if (!peopleResult.IsOK) {

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return Mapper.Map<PaginatedList<Person>, PaginatedDto<PersonDto>>(peopleResult.Result);
        }

        public PersonDto GetPerson(int id) {

            OperationResult<Person> personResult = _peopleService.GetById(id);
            if (!personResult.IsOK) {

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return Mapper.Map<Person, PersonDto>(personResult.Result);
        }

        public HttpResponseMessage PostPerson(PersonRequestModel requestModel) {

            var person = Mapper.Map<PersonRequestModel, Person>(requestModel);
            OperationResult<Person> personResult = _peopleService.AddNewUser(person);
            if (!personResult.IsOK) {

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            var personDto = Mapper.Map<Person, PersonDto>(personResult.Result);
            return Request.CreateResponse(HttpStatusCode.Created, personDto);
        }

        public PersonDto PutPerson(int id, PersonRequestModel requestModel) {

            var person = Mapper.Map<PersonRequestModel, Person>(requestModel);
            OperationResult<Person> personResult = _peopleService.UpdateUser(person);
            if (!personResult.IsOK) {

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return Mapper.Map<Person, PersonDto>(personResult.Result);
        }
    }
}