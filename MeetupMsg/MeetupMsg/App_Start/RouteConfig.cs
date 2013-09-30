using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MeetupMsg
    {
    public class RouteConfig
        {
        public static void RegisterRoutes(RouteCollection routes)
            {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "auth", // Route name
               "auth", // URL with parameters
               new { controller = "Auth", action = "StartAuth" } // Parameter defaults
           );

            routes.MapRoute(
                "oauth2callback", // Route name
                "oauth2callback", // URL with parameters
                new { controller = "Auth", action = "OAuth2Callback" } // Parameter defaults
            );

            routes.MapRoute(
              "notify", // Route name
              "notify", // URL with parameters
              new { controller = "Notify", action = "Notify" } // Parameter defaults
          );
            routes.MapRoute(
             "meetup", // Route name
             "meetup", // URL with parameters
             new { controller = "Meetup", action = "Index" } // Parameter defaults
         );
/*
            routes.MapRoute(
                "default", // Route name
                "", // URL with parameters
                new { controller = "Main", action = "Index" } // Parameter defaults
            );*/


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            }
        }
    }