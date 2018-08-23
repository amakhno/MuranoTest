using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using Entity;
using Interfaces.Repositories;
using Interfaces.Services;
using MuranoTest.Web.Filters;
using Services;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Mvc;

namespace MuranoTest.Web
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.Register(c => new ApplicationContext()).SingleInstance();
            builder.RegisterType<LoggerService>().As<ILoggerService>();
            builder.Register(c=> new Logger(c.Resolve<ILoggerService>()))
                .AsWebApiActionFilterFor<ApiController>().InstancePerRequest();
            builder.Register(c => new Logger(c.Resolve<ILoggerService>()))
                .AsActionFilterFor<Controller>().InstancePerRequest();
            builder.RegisterType<SearchConnectorRepository>().As<ISearchConnectorRepository>();
            builder.RegisterType<SearchResultPositionRepository>().As<ISearchResultPositionRepository>();
            builder.RegisterType<SearchService>().As<ISearchService>();
            builder.RegisterType<SearchConnectorService>().As<ISearchConnectorService>();

            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterFilterProvider();

            var container = builder.Build();
            var webApiResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }
    }
}
