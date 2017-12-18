namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class raiserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "Answerer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Raiser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AnswerComments", "Commenter_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AnswerComments", "ParentAnswer_Id", "dbo.Answers");
            DropForeignKey("dbo.Answers", "ParentPost_Id", "dbo.Posts");
            DropIndex("dbo.AnswerComments", new[] { "Commenter_Id" });
            DropIndex("dbo.AnswerComments", new[] { "ParentAnswer_Id" });
            DropIndex("dbo.Answers", new[] { "Answerer_Id" });
            DropIndex("dbo.Answers", new[] { "ParentPost_Id" });
            DropIndex("dbo.Posts", new[] { "Raiser_Id" });
            AddColumn("dbo.Answers", "AnswererId", c => c.String(nullable: false));
            AddColumn("dbo.Posts", "RaiserId", c => c.String(nullable: false));
            AlterColumn("dbo.AnswerComments", "Body", c => c.String(nullable: false));
            AlterColumn("dbo.AnswerComments", "Commenter_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AnswerComments", "ParentAnswer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "Body", c => c.String(nullable: false));
            AlterColumn("dbo.Answers", "ParentPost_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Posts", "Question", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false));
            CreateIndex("dbo.AnswerComments", "Commenter_Id");
            CreateIndex("dbo.AnswerComments", "ParentAnswer_Id");
            CreateIndex("dbo.Answers", "ParentPost_Id");
            AddForeignKey("dbo.AnswerComments", "Commenter_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AnswerComments", "ParentAnswer_Id", "dbo.Answers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "ParentPost_Id", "dbo.Posts", "Id", cascadeDelete: true);
            DropColumn("dbo.Answers", "Answerer_Id");
            DropColumn("dbo.Posts", "Raiser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Raiser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Answers", "Answerer_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Answers", "ParentPost_Id", "dbo.Posts");
            DropForeignKey("dbo.AnswerComments", "ParentAnswer_Id", "dbo.Answers");
            DropForeignKey("dbo.AnswerComments", "Commenter_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Answers", new[] { "ParentPost_Id" });
            DropIndex("dbo.AnswerComments", new[] { "ParentAnswer_Id" });
            DropIndex("dbo.AnswerComments", new[] { "Commenter_Id" });
            AlterColumn("dbo.Posts", "Description", c => c.String());
            AlterColumn("dbo.Posts", "Question", c => c.String());
            AlterColumn("dbo.Answers", "ParentPost_Id", c => c.Int());
            AlterColumn("dbo.Answers", "Body", c => c.String());
            AlterColumn("dbo.AnswerComments", "ParentAnswer_Id", c => c.Int());
            AlterColumn("dbo.AnswerComments", "Commenter_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.AnswerComments", "Body", c => c.String());
            DropColumn("dbo.Posts", "RaiserId");
            DropColumn("dbo.Answers", "AnswererId");
            CreateIndex("dbo.Posts", "Raiser_Id");
            CreateIndex("dbo.Answers", "ParentPost_Id");
            CreateIndex("dbo.Answers", "Answerer_Id");
            CreateIndex("dbo.AnswerComments", "ParentAnswer_Id");
            CreateIndex("dbo.AnswerComments", "Commenter_Id");
            AddForeignKey("dbo.Answers", "ParentPost_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.AnswerComments", "ParentAnswer_Id", "dbo.Answers", "Id");
            AddForeignKey("dbo.AnswerComments", "Commenter_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "Raiser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Answers", "Answerer_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
