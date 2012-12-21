using RestPOC.API.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestPOC.API.Config {

    public class RouteConfig {

        public static void RegisterRoutes(HttpConfiguration config) {

            HttpRouteCollection routes = config.Routes;

            routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{apikey}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { apikey = new GuidRouteConstraint() });
        }
    }
}