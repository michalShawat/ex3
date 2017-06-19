namespace Maze.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Maze.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Maze.Models.MazeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Maze.Models.MazeContext context)
        {
            context.Authors.AddOrUpdate(x => x.Id, new Author()
                                                       {
                                                           Id = 1, Name = "Jane Austen"
                                                       }, 
                                                       new Author()
                                                           {
                                                               Id = 2, Name = "Charles Dickens"
                                                           }, 
                                                       new Author()
                                                           {
                                                               Id = 3, Name = "Miguel de Cervantes"
                                                           });
            context.Books.AddOrUpdate(x => x.Id,
                new Book()
                    {
                        Id = 1, Title = "Pride and Prejudice", Year = 1813, AuthorId = 1, Price = 9.99M, Genre = "Comedy of manners"
                    }, 
                new Book()
                    {
                        Id = 2, Title = "Northanger Abbey", Year = 1817, AuthorId = 1, Price = 12.95M, Genre = "Gothic parody"
                    }
                 );
        }
    }
}
