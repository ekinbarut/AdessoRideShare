namespace ARS.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Travels",
                c => new
                    {
                        TravelId = c.Int(nullable: false, identity: true),
                        DriverId = c.Int(nullable: false),
                        Description = c.String(),
                        SeatCount = c.Int(nullable: false),
                        Departure = c.DateTime(nullable: false),
                        Arrival = c.DateTime(nullable: false),
                        DepartingCountryId = c.Int(nullable: false),
                        ArrivingCountryId = c.Int(nullable: false),
                        RideType = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Countries_CountryId = c.Int(),
                        Countries_CountryId1 = c.Int(),
                    })
                .PrimaryKey(t => t.TravelId)
                .ForeignKey("dbo.Countries", t => t.ArrivingCountryId)
                .ForeignKey("dbo.Countries", t => t.DepartingCountryId)
                .ForeignKey("dbo.Users", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Countries_CountryId)
                .ForeignKey("dbo.Countries", t => t.Countries_CountryId1)
                .Index(t => t.DriverId)
                .Index(t => t.DepartingCountryId)
                .Index(t => t.ArrivingCountryId)
                .Index(t => t.Countries_CountryId)
                .Index(t => t.Countries_CountryId1);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        MobileNumber = c.String(),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.TravelUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        TravelId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TravelId })
                .ForeignKey("dbo.Travels", t => t.TravelId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TravelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TravelUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.TravelUsers", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.Travels", "Countries_CountryId1", "dbo.Countries");
            DropForeignKey("dbo.Travels", "Countries_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Travels", "DriverId", "dbo.Users");
            DropForeignKey("dbo.Travels", "DepartingCountryId", "dbo.Countries");
            DropForeignKey("dbo.Travels", "ArrivingCountryId", "dbo.Countries");
            DropIndex("dbo.TravelUsers", new[] { "TravelId" });
            DropIndex("dbo.TravelUsers", new[] { "UserId" });
            DropIndex("dbo.Travels", new[] { "Countries_CountryId1" });
            DropIndex("dbo.Travels", new[] { "Countries_CountryId" });
            DropIndex("dbo.Travels", new[] { "ArrivingCountryId" });
            DropIndex("dbo.Travels", new[] { "DepartingCountryId" });
            DropIndex("dbo.Travels", new[] { "DriverId" });
            DropTable("dbo.TravelUsers");
            DropTable("dbo.Users");
            DropTable("dbo.Travels");
            DropTable("dbo.Countries");
        }
    }
}
