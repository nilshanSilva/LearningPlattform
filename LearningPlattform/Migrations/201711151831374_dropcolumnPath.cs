namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropcolumnPath : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "ImagePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "ImagePath", c => c.String());
        }
    }
}
