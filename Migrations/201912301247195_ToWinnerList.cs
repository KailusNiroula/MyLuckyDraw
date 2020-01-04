namespace Lucky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToWinnerList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrizeDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrizeType = c.String(nullable: false),
                        WinningNumber = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.AddWinningNumbers", "Byhand", c => c.String(maxLength: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AddWinningNumbers", "Byhand", c => c.String());
            DropTable("dbo.PrizeDetails");
        }
    }
}
