using System.Web.Http;

namespace RestPOC.API.Config {

    public class RouteConfig {

        public static void RegisterRoutes(HttpConfiguration config) {
            var routes = config.Routes;
            routes.MapHttpRoute(
                "DefaultHttpRoute",
                "v1/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}