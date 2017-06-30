using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Maze.App_Start.Startup))]

namespace Maze.App_Start
{
    /// <summary>
    ///  class that map the signalR
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
