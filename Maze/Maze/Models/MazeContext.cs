using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Maze.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class MazeContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeContext"/> class.
        /// </summary>
        public MazeContext() : base("name=MazeContext")
        {
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public System.Data.Entity.DbSet<Maze.Models.User> Users { get; set; }
    }
}
