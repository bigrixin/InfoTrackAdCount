using Autofac;
using Autofac.Integration.Mvc;
using InfoTrackAdCount.Controllers;
using InfoTrackAdCount.Service;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InfoTrackAdCount
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Register components with Autofac
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType(typeof(RetrieveSearchDataService)).AsImplementedInterfaces();
            builder.RegisterType<HomeController>().InstancePerRequest();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
