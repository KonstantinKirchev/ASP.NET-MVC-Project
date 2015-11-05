using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PhotoContests.App.App_Start;
using PhotoContests.Data;
using PhotoContests.Data.Migrations;

namespace PhotoContests.App
{
<<<<<<< HEAD
    using System.Collections.Generic;
    using System.Reflection;

    using PhotoContests.Common.Mappings;

=======
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
<<<<<<< HEAD
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
=======
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoContestsDbContext, Configuration>());
            MapperConfig.ConfigureMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
<<<<<<< HEAD
            var autoMapperConfig = new AutoMapperConfig(new List<Assembly> { Assembly.GetExecutingAssembly() });
            autoMapperConfig.Execute();
=======
>>>>>>> 97745c1ce001803da2d445f4a0a6282637aacca3
        }
    }
}
