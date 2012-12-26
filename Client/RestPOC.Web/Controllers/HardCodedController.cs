namespace RestPOC.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Text;
    using System.Web.Mvc;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using RestPOC.API.Model.Dtos;
    using RestPOC.API.Model.RequestModels;

    public class HardCodedController : Controller
    {
        private static string EncodeToBase64(string value)
        {
            var toEncodeAsBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        private IEnumerable<Task<PersonDto>> GetPersonDtos(HttpResponseMessage[] responses)
        {
            return responses.Select(httpResponseMessage => httpResponseMessage.Content.ReadAsAsync<PersonDto>());
        }

        private IEnumerable<Task<HttpResponseMessage>> GetRequests(HttpClient httpClient)
        {
            for (int i = 0; i < 5; i++)
            {

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44307/v1/people");
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", EncodeToBase64(string.Format("{0}:{1}", "user", "password")));
                requestMessage.Headers.Add("X-APIKey", "apikeyforuser");

                var model = new PersonRequestModel
                {
                    Name = string.Format("Demo User {0}", i),
                    Email = string.Format("email{0}@email.com", i),
                    BirthYear = 1982
                };

                requestMessage.Content = new ObjectContent<PersonRequestModel>(model, new JsonMediaTypeFormatter());

                yield return httpClient.SendAsync(requestMessage);
            }
        }

        public async Task<ActionResult> GenerateDemoData()
        {
            // HttpResponseMessage PostPerson(PersonRequestModel requestModel)

            using (var httpClient = new HttpClient()) {
                var requestTasks = this.GetRequests(httpClient);
                var responses = await Task.WhenAll(requestTasks);
                var contents = await Task.WhenAll(this.GetPersonDtos(responses));

                return this.View(contents);
            }
        }

       

        public async Task<ActionResult> Index()
        {
            // public access

            // public PersonDto GetPerson(int id)

            var client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44307/v1/people/8");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(requestMessage);

            var content = await response.Content.ReadAsAsync<PersonDto>();

            return View(content);
        }

        public async Task<ActionResult> UserRole()
        {
            // apikeyforuser
            // user
            // password

            var client = new HttpClient();

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "https://localhost:44307/v1/people/8");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", EncodeToBase64(string.Format("{0}:{1}", "user", "password")));
            requestMessage.Headers.Add("X-APIKey", "apikeyforuser");
            
            var model = new PersonRequestModel
            {
                Name = "Updated Demo User 1",
                Email = "email1@email.com",
                BirthYear = 1982
            };

            requestMessage.Content = new ObjectContent<PersonRequestModel>(model, new JsonMediaTypeFormatter());
            
            var response = await client.SendAsync(requestMessage);
            var content = await response.Content.ReadAsAsync<PersonDto>();
            
            return View(content);
        }

        public async Task<ActionResult> SuperUserRole()
        {
            // Super User Geliyosa...
            // public PaginatedDto<PersonDto> GetPeople(PaginatedRequestCommand cmd)

            // apikeyforsuperuser
            // superuser
            // password


            var client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44307/v1/people?pagesize=2&pageindex=1");
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Headers.Add("Authorization:Basic", "c3VwZXJ1c2VyOnBhc3N3b3Jk");
            requestMessage.Headers.Add("X-APIKey", "apikeyforsuperuser");

            var response = await client.SendAsync(requestMessage);

            var content = await response.Content.ReadAsAsync<PaginatedDto<PersonDto>>();

            return View(content);
        }
    }
}
