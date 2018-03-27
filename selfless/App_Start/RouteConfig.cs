using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;

namespace Selfless
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Conta", action = "Registrar", id = UrlParameter.Optional },
                namespaces: new[] { "Selfless.Controllers" }


        );



        }
    }
}
