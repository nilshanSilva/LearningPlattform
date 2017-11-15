namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseImage1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "ImagePath", c => c.String(nullable: false));
        }
    }
}
