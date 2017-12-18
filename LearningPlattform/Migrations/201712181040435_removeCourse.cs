namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeCourse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Posts", new[] { "Course_Id" });
            DropColumn("dbo.Posts", "Course_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Course_Id", c => c.Int());
            CreateIndex("dbo.Posts", "Course_Id");
            AddForeignKey("dbo.Posts", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
