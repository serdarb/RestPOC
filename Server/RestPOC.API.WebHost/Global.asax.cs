using RestPOC.API.Config;
using System;
using System.Web.Http;

namespace RestPOC.API.WebHost {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            HttpConfiguration config = GlobalConfiguration.Configuration;

            // General Web API configuraton
            AutofacWebAPI.Initialize(config);
            WebAPIConfig.Configure(config, registerTracer: true);
            RouteConfig.RegisterRoutes(config);
            AutoMapperConfig.Configure();
        }
    }
}