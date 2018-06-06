namespace NewsCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleModels", "AuthorId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ArticleModels", "AuthorId", c => c.Int(nullable: false));
        }
    }
}
