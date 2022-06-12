namespace FriendOrganizer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friend", "FavoriteLanguageId", "dbo.ProgrammingLanguage");
            DropForeignKey("dbo.FriendPhoneNumber", "FriendId", "dbo.Friend");
            DropIndex("dbo.FriendPhoneNumber", new[] { "FriendId" });
            DropIndex("dbo.Friend", new[] { "FavoriteLanguageId" });
            DropColumn("dbo.Friend", "FavoriteLanguageId");
            DropTable("dbo.FriendPhoneNumber");
            DropTable("dbo.ProgrammingLanguage");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProgrammingLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FriendPhoneNumber",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false),
                        FriendId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Friend", "FavoriteLanguageId", c => c.Int());
            CreateIndex("dbo.Friend", "FavoriteLanguageId");
            CreateIndex("dbo.FriendPhoneNumber", "FriendId");
            AddForeignKey("dbo.FriendPhoneNumber", "FriendId", "dbo.Friend", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Friend", "FavoriteLanguageId", "dbo.ProgrammingLanguage", "Id");
        }
    }
}
