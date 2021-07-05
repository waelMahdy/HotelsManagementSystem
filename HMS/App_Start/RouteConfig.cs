using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "FEAccomodations",
            url: "Accomodation",
            defaults: new { area = "", controller = "Accomodation", action = "Index" },
            namespaces: new[] { "HMS.Controllers" }
        );
            routes.MapRoute(
            name: "AccomodationPackageDetails",
            url: "Accomodation-Package/{accomodationPackageID}",
            defaults: new { controller = "Accomodation", action = "Details" },
            namespaces: new[] { "HMS.Controllers" }
        );
            routes.MapRoute(
            name: "Accomodations",
            url: "All-Rooms/{accomodationPackageID}",
            defaults: new { controller = "Accomodation", action = "ShowAllAccomodation" },
            namespaces: new[] { "HMS.Controllers" }
         );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
