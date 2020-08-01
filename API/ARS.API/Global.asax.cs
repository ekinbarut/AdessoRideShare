using ARS.Service;
using ARS.Service.Interfaces;
using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ARS.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            #region [ Dependency Injection ]

            var container = new ServiceContainer();
            container.RegisterApiControllers();

            //register other services
            container.EnableWebApi(GlobalConfiguration.Configuration);

            // Register your types, for instanceScoped);
            container.Register<IARSService, ARSService>();

            #endregion
        }
    }
}
