namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posteddata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Answer_Id", "dbo.Answers");
            DropIndex("dbo.AspNetUsers", new[] { "Answer_Id" });
            AddColumn("dbo.Posts", "PostedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "Answer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Answer_Id", c => c.Int());
            DropColumn("dbo.Posts", "PostedDate");
            CreateIndex("dbo.AspNetUsers", "Answer_Id");
            AddForeignKey("dbo.AspNetUsers", "Answer_Id", "dbo.Answers", "Id");
        }
    }
}
