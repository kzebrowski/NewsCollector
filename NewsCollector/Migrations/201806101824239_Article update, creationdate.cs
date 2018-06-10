namespace NewsCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Articleupdatecreationdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleModels", "AdditionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleModels", "AdditionDate");
        }
    }
}
