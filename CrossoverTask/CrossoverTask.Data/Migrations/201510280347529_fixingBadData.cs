namespace CrossoverTask.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingBadData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Convicts", "DateOfBirth", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Convicts", "DateOfBirth");
        }
    }
}
