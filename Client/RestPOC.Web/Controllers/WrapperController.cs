namespace RestPOC.Web.Controllers
{
    using System.Web.Mvc;
    using System.Threading.Tasks;

    using RestPOC.API.Model.RequestModels;
    using RestPOC.API.Wrapper.Net;

    public class WrapperController : Controller
    {
        private readonly IPeopleClient client;
        public WrapperController(IPeopleClient client) {
            this.client = client;
        }

        public async Task<ActionResult> Index() {
            var response = await client.GetPerson(8);
            return View("~/Views/HardCoded/Index.cshtml", response.Model);
        }

        public async Task<ActionResult> UserRole()
        {
            var model = new PersonRequestModel
            {
                Name = "Updated Demo User 1",
                Email = "email1@email.com",
                BirthYear = 1982
            };

            var response = await client.UpdatePerson(8, model);
            return View("~/Views/HardCoded/UserRole.cshtml", response.Model);
        }

        public async Task<ActionResult> SuperUserRole() {
            var response = await client.GetPeople(1, 5);
            return View("~/Views/HardCoded/SuperUserRole.cshtml", response.Model);
        }

    }
}
