namespace MeetupMsg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class meetup001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoredCredentials",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        AccessToken = c.String(),
                        RefreshToken = c.String(),
                        TokenIssuedTime = c.DateTime(),
                        TokenExpirationTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StoredCredentials");
        }
    }
}
