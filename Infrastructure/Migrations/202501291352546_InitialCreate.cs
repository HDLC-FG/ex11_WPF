namespace Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Engines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Horsepower = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Brand = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Engine_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Engines", t => t.Engine_Id, cascadeDelete: true)
                .Index(t => t.Engine_Id);
            
            CreateTable(
                "dbo.VehicleOptions",
                c => new
                    {
                        Vehicle_Id = c.Int(nullable: false),
                        Option_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vehicle_Id, t.Option_Id })
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id, cascadeDelete: true)
                .ForeignKey("dbo.Options", t => t.Option_Id, cascadeDelete: true)
                .Index(t => t.Vehicle_Id)
                .Index(t => t.Option_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleOptions", "Option_Id", "dbo.Options");
            DropForeignKey("dbo.VehicleOptions", "Vehicle_Id", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "Engine_Id", "dbo.Engines");
            DropIndex("dbo.VehicleOptions", new[] { "Option_Id" });
            DropIndex("dbo.VehicleOptions", new[] { "Vehicle_Id" });
            DropIndex("dbo.Vehicles", new[] { "Engine_Id" });
            DropTable("dbo.VehicleOptions");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Options");
            DropTable("dbo.Engines");
        }
    }
}
