namespace ARS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBFix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Travels", new[] { "ParentId" });
            AlterColumn("dbo.Travels", "ParentId", c => c.Int());
            CreateIndex("dbo.Travels", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Travels", new[] { "ParentId" });
            AlterColumn("dbo.Travels", "ParentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Travels", "ParentId");
        }
    }
}
