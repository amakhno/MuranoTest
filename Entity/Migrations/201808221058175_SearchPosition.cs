namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SearchPosition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SearchResultPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Link = c.String(nullable: false),
                        Description = c.String(),
                        Query = c.String(nullable: false),
                        SearchConnectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SearchConnectors", t => t.SearchConnectorId, cascadeDelete: true)
                .Index(t => t.SearchConnectorId);
            
            AlterColumn("dbo.SearchConnectors", "DirectLink", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SearchResultPositions", "SearchConnectorId", "dbo.SearchConnectors");
            DropIndex("dbo.SearchResultPositions", new[] { "SearchConnectorId" });
            AlterColumn("dbo.SearchConnectors", "DirectLink", c => c.String(nullable: false));
            DropTable("dbo.SearchResultPositions");
        }
    }
}
