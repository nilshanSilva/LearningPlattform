namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Category = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        CourseLevel = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Path = c.String(),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Path = c.String(),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Documents", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.CourseComments", "Course_Id", "dbo.Courses");
            DropIndex("dbo.Videos", new[] { "Course_Id" });
            DropIndex("dbo.Documents", new[] { "Course_Id" });
            DropIndex("dbo.CourseComments", new[] { "Course_Id" });
            DropTable("dbo.Videos");
            DropTable("dbo.Documents");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseComments");
        }
    }
}
