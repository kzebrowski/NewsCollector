namespace NewsCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArticleUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleModels", "Image", c => c.Binary());
            DropColumn("dbo.ArticleModels", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArticleModels", "ImagePath", c => c.String());
            DropColumn("dbo.ArticleModels", "Image");
        }
    }
}
