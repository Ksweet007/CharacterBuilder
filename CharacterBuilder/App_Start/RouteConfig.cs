using System.Web.Mvc;
using System.Web.Routing;

namespace CharacterBuilder
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "CharacterBuilder", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "LogOut",
                url: "auth/{action}",
                defaults: new { controller = "Account", action = "LogOff" }
            );

            routes.MapRoute(
                name: "EmptyDefault",
                url: "",
                defaults: new { controller = "CharacterBuilder", action = "Index" }
            );
        }
    }
}
