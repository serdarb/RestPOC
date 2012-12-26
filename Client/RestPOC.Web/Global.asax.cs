namespace RestPOC.Web
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    using RestPOC.API.Wrapper.Net;
    using RestPOC.API.Wrapper.Net.Configuration;
    using RestPOC.Web.Controllers;


    public class MvcApplication : System.Web.HttpApplication
    {
        public static ApiClientContext ApiClientContext { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ApiClientContext = ApiClientContext.Create((cnf) =>cnf.SetCredentialsFromAppSetting("username", "password", "apikey").ConnectTo("http://localhost:44307/"));

            BootstrapContainer();
        }

        private static IWindsorContainer _container;

        private static void BootstrapContainer()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }

    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return (IController)_kernel.Resolve(controllerType);
        }
    }

    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPeopleClient>()
                                        .Instance(MvcApplication.ApiClientContext.GePeopleClient())
                                        .LifestyleSingleton());
        }
    }

    public class ControllerInstaller
    {
        public class ControllersInstaller : IWindsorInstaller
        {
            public void Install(IWindsorContainer container, IConfigurationStore store)
            {
                container.Register(Types.FromThisAssembly()
                                    .Pick().If(Component.IsInSameNamespaceAs<HardCodedController>())
                                    .If(t => t.Name.EndsWith("Controller"))
                                    .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                                    .LifestylePerWebRequest());
            }
        }
    }
}