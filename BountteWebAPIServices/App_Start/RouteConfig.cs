using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Routing;

namespace BountteWebAPIServices
{
    
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


           // routes.MapRoute(
           //    name: "SingleProduct",
           //    url: "api/all/single/{prodid}",
           //    defaults: new { controller = "Products", action = "GetProduct", prodid = UrlParameter.Optional }
           //).RouteHandler = new SessionStateRouteHandler();


          //  routes.MapRoute(
          //    name: "AddToCart",
          //    url: "api/all/updatecart/{qty}",
          //    defaults: new { controller = "Cart", action = "PostToCart", qty = UrlParameter.Optional }
          //);//.RouteHandler = new SessionStateRouteHandler();


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            ); //.RouteHandler = new SessionStateRouteHandler();

           



        }
    }
}
