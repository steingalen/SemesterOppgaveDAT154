namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        Slutt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomSizeId = c.Int(nullable: false),
                        RoomBedsId = c.Int(nullable: false),
                        RoomQualityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoomBeds", t => t.RoomBedsId, cascadeDelete: true)
                .ForeignKey("dbo.RoomQualities", t => t.RoomQualityId, cascadeDelete: true)
                .ForeignKey("dbo.RoomSizes", t => t.RoomSizeId, cascadeDelete: true)
                .Index(t => t.RoomSizeId)
                .Index(t => t.RoomBedsId)
                .Index(t => t.RoomQualityId);
            
            CreateTable(
                "dbo.RoomBeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Beds = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomQualities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quality = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        TaskTypeId = c.Int(nullable: false),
                        Status = c.String(),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.TaskTypes", t => t.TaskTypeId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.TaskTypeId);
            
            CreateTable(
                "dbo.TaskTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomTasks", "TaskTypeId", "dbo.TaskTypes");
            DropForeignKey("dbo.RoomTasks", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "RoomSizeId", "dbo.RoomSizes");
            DropForeignKey("dbo.Rooms", "RoomQualityId", "dbo.RoomQualities");
            DropForeignKey("dbo.Rooms", "RoomBedsId", "dbo.RoomBeds");
            DropForeignKey("dbo.Reservations", "CustomerId", "dbo.Customers");
            DropIndex("dbo.RoomTasks", new[] { "TaskTypeId" });
            DropIndex("dbo.RoomTasks", new[] { "RoomId" });
            DropIndex("dbo.Rooms", new[] { "RoomQualityId" });
            DropIndex("dbo.Rooms", new[] { "RoomBedsId" });
            DropIndex("dbo.Rooms", new[] { "RoomSizeId" });
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            DropIndex("dbo.Reservations", new[] { "CustomerId" });
            DropTable("dbo.TaskTypes");
            DropTable("dbo.RoomTasks");
            DropTable("dbo.RoomSizes");
            DropTable("dbo.RoomQualities");
            DropTable("dbo.RoomBeds");
            DropTable("dbo.Rooms");
            DropTable("dbo.Reservations");
            DropTable("dbo.Customers");
        }
    }
}
