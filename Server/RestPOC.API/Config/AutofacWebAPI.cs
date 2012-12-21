using Autofac;
using Autofac.Integration.WebApi;
using RestPOC.Domain;
using RestPOC.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace RestPOC.API.Config {
    
    public class AutofacWebAPI {

        public static void Initialize(HttpConfiguration config) {

            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container) {

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder) {

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // 1-) DbCotext registration
            builder.Register(c => new PeopleDB()).As<DbContext>().InstancePerApiRequest();

            // 2-) Repositories
            RegisterRepositories(builder);

            // 3-) Services
            builder.RegisterType<PeopleService>().As<IPeopleService>().InstancePerApiRequest();

            return builder.Build();
        }

        private static void RegisterRepositories(ContainerBuilder builder) {

            Type baseEntityType = typeof(IEntity);
            Assembly assembly = baseEntityType.Assembly;
            IEnumerable<Type> entityTypes = assembly.GetTypes().Where(x => x.IsAssignableFrom(baseEntityType));
            foreach (Type type in entityTypes) {

                builder.RegisterType(typeof(EntityRepository<>).MakeGenericType(type)).As(typeof(IEntityRepository<>).MakeGenericType(type)).InstancePerApiRequest();
            }
        }
    }
}