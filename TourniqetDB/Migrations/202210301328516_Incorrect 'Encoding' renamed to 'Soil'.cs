namespace TourniqetDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncorrectEncodingrenamedtoSoil : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStudAccs", "Soil", c => c.String());
            DropColumn("dbo.tblStudAccs", "Encoding");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblStudAccs", "Encoding", c => c.String());
            DropColumn("dbo.tblStudAccs", "Soil");
        }
    }
}
