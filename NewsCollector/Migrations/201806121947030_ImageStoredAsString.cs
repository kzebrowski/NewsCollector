namespace NewsCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageStoredAsString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleModels", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ArticleModels", "Image", c => c.Binary());
        }
    }
}
