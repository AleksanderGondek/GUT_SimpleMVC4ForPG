using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common;
using SimpleMVC4.Context;

namespace SimpleMVC4
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //DbInitialization, with magic concering SimpleAuth
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SimpleMvc4Context, DatabaseInitializator>());

            //Eager-ish start
            //var db = new SimpleMvc4Context();
           // db.Database.Initialize(false);
            //db.UserProfiles.Find(1);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<ISimpleMvc4Context>().To<SimpleMvc4Context>();
            return kernel;
        }
    }
}