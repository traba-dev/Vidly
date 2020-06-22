using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //ruta dinamica
            routes.MapMvcAttributeRoutes();
            //ruta manual
            //routes.MapRoute(
            //    "MoviesByReleaseDate",
            //    "movies/release/{year}/{month}",
            //    new { controller="Movies",action="ByReleaseDate"}
            //    );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Customer", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
