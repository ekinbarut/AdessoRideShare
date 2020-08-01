namespace ARS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBImprovement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Travels", "ParentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Travels", "ParentId");
            AddForeignKey("dbo.Travels", "ParentId", "dbo.Travels", "TravelId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Travels", "ParentId", "dbo.Travels");
            DropIndex("dbo.Travels", new[] { "ParentId" });
            DropColumn("dbo.Travels", "ParentId");
        }
    }
}
