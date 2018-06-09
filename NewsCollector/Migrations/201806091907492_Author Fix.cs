namespace NewsCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuthorFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ArticleModels", "AuthorId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ArticleModels", "AuthorId");
            AddForeignKey("dbo.ArticleModels", "AuthorId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleModels", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.ArticleModels", new[] { "AuthorId" });
            AlterColumn("dbo.ArticleModels", "AuthorId", c => c.Guid(nullable: false));
        }
    }
}
