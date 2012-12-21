namespace RestPOC.API.MessageHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;

    public class BasicAuthMessageHandler : WebAPIDoodle.Http.BasicAuthenticationHandler
    {
        protected override Task<IPrincipal> AuthenticateUserAsync(HttpRequestMessage request, string username, string password, CancellationToken cancellationToken)
        {
            IEnumerable<string> apiKeyHeaderValues;
            if (request.Headers.TryGetValues("X-APIKey", out apiKeyHeaderValues))
            {
                var key = apiKeyHeaderValues.First();
                if (!string.IsNullOrEmpty(key))
                {
                    if ((username == "user" && key == "apikeyforuser" && password == "password") 
                        || (username == "superuser" && key == "apikeyforsuperuser" && password == "password"))
                    {
                        var identity = new GenericIdentity(username);
                        return Task.FromResult<IPrincipal>(new GenericPrincipal(identity, new[] { username }));
                    }
                }
            }

            return Task.FromResult<IPrincipal>(null);
        }

        protected override void HandleUnauthenticatedRequest(WebAPIDoodle.Http.UnauthenticatedRequestContext context)
        {

        }
    }
};