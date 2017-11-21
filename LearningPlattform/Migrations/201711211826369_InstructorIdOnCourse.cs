namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstructorIdOnCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "InstructorId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "InstructorId");
        }
    }
}
