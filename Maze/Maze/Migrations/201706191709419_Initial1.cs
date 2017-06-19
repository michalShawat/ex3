namespace Maze.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false));
        }
    }
}
