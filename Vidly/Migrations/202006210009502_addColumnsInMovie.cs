namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnsInMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Genre", c => c.String(nullable: false));
            AddColumn("dbo.Movies", "RealeaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "DateAdd", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberInStock");
            DropColumn("dbo.Movies", "DateAdd");
            DropColumn("dbo.Movies", "RealeaseDate");
            DropColumn("dbo.Movies", "Genre");
        }
    }
}
