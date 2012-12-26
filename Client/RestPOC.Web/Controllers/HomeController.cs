using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestPOC.Web.Controllers
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using RestPOC.API.Model.Dtos;

    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public async Task<ActionResult> Index()
        {
            // Super User Geliyosa...
            // OperationResult<PaginatedList<Person>> GetAll(int pageIndex, int pageSize);

            // apikeyforuser
            // user
            // password


            // apikeyforsuperuser
            // superuser
            // password


            var client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44307/v1/people/1");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(requestMessage);

            var content = await response.Content.ReadAsAsync<PersonDto>();

            return View(content);
        }

    }
}
