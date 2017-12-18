namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forum : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CourseComments", name: "User_Id", newName: "Commenter_Id");
            RenameIndex(table: "dbo.CourseComments", name: "IX_User_Id", newName: "IX_Commenter_Id");
            CreateTable(
                "dbo.AnswerComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Commenter_Id = c.String(maxLength: 128),
                        ParentAnswer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Commenter_Id)
                .ForeignKey("dbo.Answers", t => t.ParentAnswer_Id)
                .Index(t => t.Commenter_Id)
                .Index(t => t.ParentAnswer_Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        CorrectAnswer = c.Boolean(nullable: false),
                        Answerer_Id = c.String(maxLength: 128),
                        Post_Id = c.Int(),
                        ParentPost_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Answerer_Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .ForeignKey("dbo.Posts", t => t.ParentPost_Id)
                .Index(t => t.Answerer_Id)
                .Index(t => t.Post_Id)
                .Index(t => t.ParentPost_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Description = c.String(),
                        CorrectAnswer_Id = c.Int(),
                        Course_Id = c.Int(),
                        Raiser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.CorrectAnswer_Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Raiser_Id)
                .Index(t => t.CorrectAnswer_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Raiser_Id);
            
            AddColumn("dbo.AspNetUsers", "Answer_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Answer_Id");
            AddForeignKey("dbo.AspNetUsers", "Answer_Id", "dbo.Answers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "ParentPost_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Raiser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Posts", "CorrectAnswer_Id", "dbo.Answers");
            DropForeignKey("dbo.Answers", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.AspNetUsers", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.AnswerComments", "ParentAnswer_Id", "dbo.Answers");
            DropForeignKey("dbo.Answers", "Answerer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AnswerComments", "Commenter_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "Raiser_Id" });
            DropIndex("dbo.Posts", new[] { "Course_Id" });
            DropIndex("dbo.Posts", new[] { "CorrectAnswer_Id" });
            DropIndex("dbo.Answers", new[] { "ParentPost_Id" });
            DropIndex("dbo.Answers", new[] { "Post_Id" });
            DropIndex("dbo.Answers", new[] { "Answerer_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Answer_Id" });
            DropIndex("dbo.AnswerComments", new[] { "ParentAnswer_Id" });
            DropIndex("dbo.AnswerComments", new[] { "Commenter_Id" });
            DropColumn("dbo.AspNetUsers", "Answer_Id");
            DropTable("dbo.Posts");
            DropTable("dbo.Answers");
            DropTable("dbo.AnswerComments");
            RenameIndex(table: "dbo.CourseComments", name: "IX_Commenter_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.CourseComments", name: "Commenter_Id", newName: "User_Id");
        }
    }
}
