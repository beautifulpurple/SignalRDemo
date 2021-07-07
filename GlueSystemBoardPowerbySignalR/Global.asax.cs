using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.SqlClient;
using System.Configuration;

namespace GlueSystemBoardPowerbySignalR
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Æô¶¯sqldependency  
            SqlDependency.Start(ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString);

        }
        protected void Application_End(object sender, EventArgs e)
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString);
        }
    }
}
