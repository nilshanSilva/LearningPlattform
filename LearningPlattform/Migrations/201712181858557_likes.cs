namespace LearningPlattform.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikedAnswer_Id = c.Int(),
                        Liker_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.LikedAnswer_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Liker_Id)
                .Index(t => t.LikedAnswer_Id)
                .Index(t => t.Liker_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "Liker_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "LikedAnswer_Id", "dbo.Answers");
            DropIndex("dbo.Likes", new[] { "Liker_Id" });
            DropIndex("dbo.Likes", new[] { "LikedAnswer_Id" });
            DropTable("dbo.Likes");
        }
    }
}
