using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(GlueSystemBoardPowerbySignalR.App_Start.HubStartUp))]

namespace GlueSystemBoardPowerbySignalR.App_Start
{
    public class HubStartUp
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
