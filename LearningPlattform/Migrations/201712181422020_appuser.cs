namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Answerer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Answers", "Answerer_Id");
            AddForeignKey("dbo.Answers", "Answerer_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Answers", "AnswererId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "AnswererId", c => c.String(nullable: false));
            DropForeignKey("dbo.Answers", "Answerer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Answers", new[] { "Answerer_Id" });
            DropColumn("dbo.Answers", "Answerer_Id");
        }
    }
}
