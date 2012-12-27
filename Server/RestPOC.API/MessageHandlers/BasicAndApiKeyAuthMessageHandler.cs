namespace RestPOC.API.MessageHandlers {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using WebAPIDoodle.Http;

    public class BasicAndApiKeyAuthMessageHandler : BasicAuthenticationHandler {

        private class SystemUser  {
            public string Name { get; set; }
            public string Password { get; set; }
            public string ApiKey { get; set; }
            public string[] Roles { get; set; }
        }

        private readonly IEnumerable<SystemUser> Users = new List<SystemUser> {
            new SystemUser { Name = "User1", Password = "user1pass", ApiKey = "9ef0fc26-8f4d-4ba2-a67f-fe9f38ef81da", Roles = new[] { "User" } },
            new SystemUser { Name = "SuperUser1", Password = "superuser1pass", ApiKey = "9ef0fc26-8f4d-4ba2-a67f-fe9f38ef81da", Roles = new[] { "SuperUser" } }
        };

        protected override Task<IPrincipal> AuthenticateUserAsync(HttpRequestMessage request, string username, string password, CancellationToken cancellationToken) {

            IEnumerable<string> apiKeyHeaderValues = request.Headers.FirstOrDefault(x => x.Key.Equals("X-APIKey", StringComparison.InvariantCultureIgnoreCase)).Value;
            if (apiKeyHeaderValues != null) {

                string apiKey = apiKeyHeaderValues.First();
                SystemUser user = Users.FirstOrDefault(x => 
                    x.Name.Equals(username, StringComparison.InvariantCultureIgnoreCase) && x.Password == password);

                if (user != null && user.ApiKey.Equals(apiKey, StringComparison.InvariantCultureIgnoreCase)) {

                    var identity = new GenericIdentity(user.Name);
                    return Task.FromResult<IPrincipal>(new GenericPrincipal(identity, user.Roles));
                }
            }

            return Task.FromResult<IPrincipal>(null);
        }

        protected override void HandleUnauthenticatedRequest(UnauthenticatedRequestContext context) {

            // We don't set context.Response here which means that inner handler
            // will be invoked even if the request is unauthenticated.
        }
    }
}