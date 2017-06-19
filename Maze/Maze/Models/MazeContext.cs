using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Maze.Models
{
    public class MazeContext : DbContext
    {
        public MazeContext() : base("name=MazeContext")
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
