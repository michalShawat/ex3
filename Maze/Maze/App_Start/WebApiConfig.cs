using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Maze
{
    using Maze.Controllers;

    /// <summary>
    /// class of the routes
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //routes to the generate command
            config.Routes.MapHttpRoute(
        name: "Generate",
        routeTemplate: "api/{controller}/{name}/{rows}/{cols}",
        defaults: new { id = RouteParameter.Optional }
    );
            //routes to the GetUser command
            config.Routes.MapHttpRoute(
                name: "GetUser",
                routeTemplate: "api/{controller}/{action}/{name}/{password}",
                defaults: new { controller = "Users" }
            );
            //routes to the UpdateUser command
            config.Routes.MapHttpRoute(
                name: "UpdateUser",
                routeTemplate: "api/{controller}/{action}/{name}/{flag}",
                defaults: new { controller = "Users" }
);
            //defult routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //routes to the solve command
            config.Routes.MapHttpRoute(
                name: "solve",
                routeTemplate: "api/{controller}/{name}/{algorithmType}",
                defaults: new { id = RouteParameter.Optional }
            );

            //routes to the List command
            config.Routes.MapHttpRoute(
                name: "List",
                routeTemplate: "api/{controller}",
                defaults: new { id = RouteParameter.Optional }
            );


        }
    }
}