using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Maze
{
    using Maze.Controllers;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            /*config.Routes.MapHttpRoute(
            name: "GetUser",
            routeTemplate: "api/{controller}/{action}/{name}",
            defaults: new { controller = "Users" }
            );*/


            config.Routes.MapHttpRoute(
                name: "GetUser",
                routeTemplate: "api/{controller}/{action}/{name}/{password}",
                defaults: new { controller = "Users"}
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Generate",
                routeTemplate: "api/{controller}/{name}/{rows}/{cols}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "solve",
                routeTemplate: "api/{controller}/{name}/{algorithmType}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "List",
                routeTemplate: "api/{controller}",
                defaults: new { id = RouteParameter.Optional }
            );

            
        }
    }
}