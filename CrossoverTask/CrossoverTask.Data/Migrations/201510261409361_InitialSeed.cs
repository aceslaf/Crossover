namespace CrossoverTask.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSeed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Convicts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        Weight = c.Single(nullable: false),
                        Height = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Crimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConvictId = c.Int(nullable: false),
                        DateCommited = c.DateTime(nullable: false),
                        Description = c.String(),
                        Severity = c.Single(nullable: false),
                        CrimeName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Convicts", t => t.ConvictId, cascadeDelete: true)
                .Index(t => t.ConvictId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Crimes", "ConvictId", "dbo.Convicts");
            DropIndex("dbo.Crimes", new[] { "ConvictId" });
            DropTable("dbo.Crimes");
            DropTable("dbo.Convicts");
        }
    }
}
