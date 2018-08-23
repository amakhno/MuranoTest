namespace Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class directlink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchConnectors", "DirectLink", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SearchConnectors", "DirectLink");
        }
    }
}
