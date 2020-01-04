namespace Lucky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ByHandRow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddWinningNumbers", "Byhand", c => c.String());
            AlterColumn("dbo.AddWinningNumbers", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AddWinningNumbers", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.AddWinningNumbers", "Byhand");
        }
    }
}
