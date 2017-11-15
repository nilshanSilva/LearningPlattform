namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reAddColumnPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "ImagePath");
        }
    }
}
