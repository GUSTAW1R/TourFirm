namespace AbstractTourFirm_ServiceDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        CustomerLogin = c.String(nullable: false),
                        CustomerPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        TravelId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Travels", t => t.TravelId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.TravelId);
            
            CreateTable(
                "dbo.Travels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TravelName = c.String(nullable: false),
                        Additional_services = c.Boolean(nullable: false),
                        IsCreadit = c.Boolean(nullable: false),
                        Final_Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TourForTravels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TravelId = c.Int(nullable: false),
                        TourId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .ForeignKey("dbo.Travels", t => t.TravelId, cascadeDelete: true)
                .Index(t => t.TravelId)
                .Index(t => t.TourId);
            
            CreateTable(
                "dbo.Tours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TourName = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourForTravels", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.TourForTravels", "TourId", "dbo.Tours");
            DropForeignKey("dbo.Orders", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.TourForTravels", new[] { "TourId" });
            DropIndex("dbo.TourForTravels", new[] { "TravelId" });
            DropIndex("dbo.Orders", new[] { "TravelId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropTable("dbo.Tours");
            DropTable("dbo.TourForTravels");
            DropTable("dbo.Travels");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
