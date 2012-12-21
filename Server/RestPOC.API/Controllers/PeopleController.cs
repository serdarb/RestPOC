using RestPOC.API.Model.Dtos;
using RestPOC.API.Model.RequestCommands;
using RestPOC.API.Model.RequestModels;
using RestPOC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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

            throw new NotImplementedException();
        }

        public PersonDto GetPerson(int id) {

            throw new NotImplementedException();
        }

        public HttpResponseMessage PostPerson(PersonRequestModel requestModel) {

            throw new NotImplementedException();
        }

        public PersonDto PutPerson(int id, PersonUpdateRequestModel requestModel) {

            throw new NotImplementedException();
        }
    }
}