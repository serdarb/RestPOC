namespace RestPOC.Web.Controllers
{
    using System.Web.Mvc;
    using System.Threading.Tasks;

    using RestPOC.API.Model.RequestModels;
    using RestPOC.API.Wrapper.Net;
    using RestPOC.API.Model.Dtos;

    public class WrapperController : Controller {

        private readonly IPeopleClient _peopleClient;
        public WrapperController(IPeopleClient peopleClient) {

            _peopleClient = peopleClient;
        }

        public async Task<ActionResult> Index() {

            HttpApiResponseMessage<PersonDto> apiResponse = await _peopleClient.GetPersonAsync(8);
            return View("~/Views/HardCoded/Index.cshtml", apiResponse.Model);
        }

        public async Task<ActionResult> UserRole() {

            var model = new PersonRequestModel {
                Name = "Updated Demo User 1",
                Email = "email1@email.com",
                BirthYear = 1982
            };

            HttpApiResponseMessage<PersonDto> apiResponse = 
                await _peopleClient.UpdatePersonAsync(8, model);

            return View("~/Views/HardCoded/UserRole.cshtml", apiResponse.Model);
        }

        public async Task<ActionResult> SuperUserRole() {

            HttpApiResponseMessage<PaginatedDto<PersonDto>> apiResponse = 
                await _peopleClient.GetPeopleAsync(1, 5);

            return View("~/Views/HardCoded/SuperUserRole.cshtml", apiResponse.Model);
        }
    }
}