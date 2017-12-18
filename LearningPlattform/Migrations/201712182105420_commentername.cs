namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnswerComments", "CommenterName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnswerComments", "CommenterName");
        }
    }
}
