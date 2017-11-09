namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrollments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Course_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Course_Id);
            
            AddColumn("dbo.CourseComments", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "University", c => c.String());
            AddColumn("dbo.AspNetUsers", "AccountStatus", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserLevel", c => c.Int(nullable: false));
            CreateIndex("dbo.CourseComments", "User_Id");
            AddForeignKey("dbo.CourseComments", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseComments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.ApplicationUserCourses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserCourses", new[] { "Course_Id" });
            DropIndex("dbo.ApplicationUserCourses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CourseComments", new[] { "User_Id" });
            DropColumn("dbo.AspNetUsers", "UserLevel");
            DropColumn("dbo.AspNetUsers", "AccountStatus");
            DropColumn("dbo.AspNetUsers", "University");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.CourseComments", "User_Id");
            DropTable("dbo.ApplicationUserCourses");
        }
    }
}
