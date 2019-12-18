namespace MyMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MiMarket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyingProducts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NumberBuying = c.Int(nullable: false),
                        UnitPriceBuying = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountBuying = c.Int(nullable: false),
                        IsLoading = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ProductId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 256),
                        Code = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdentificationNumber = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 256),
                        Mail = c.String(maxLength: 500),
                        PhoneNumber = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CodeReceipt = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationTime = c.DateTime(nullable: false),
                        CustomerId = c.Long(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CustomerId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SaleProducts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Amount = c.Int(nullable: false),
                        IsLoading = c.Boolean(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationTime = c.DateTime(nullable: false),
                        ProductId = c.Long(nullable: false),
                        ReceiptId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Receipts", t => t.ReceiptId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ReceiptId);
            
            CreateTable(
                "dbo.WareHouses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AmountWareHouse = c.Int(nullable: false),
                        UnitPriceBuying = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MargenGain = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreationTime = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ProductId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WareHouses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WareHouses", "ProductId", "dbo.Products");
            DropForeignKey("dbo.SaleProducts", "ReceiptId", "dbo.Receipts");
            DropForeignKey("dbo.SaleProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Receipts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Receipts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BuyingProducts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BuyingProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WareHouses", new[] { "ProductId" });
            DropIndex("dbo.WareHouses", new[] { "UserId" });
            DropIndex("dbo.SaleProducts", new[] { "ReceiptId" });
            DropIndex("dbo.SaleProducts", new[] { "ProductId" });
            DropIndex("dbo.Receipts", new[] { "UserId" });
            DropIndex("dbo.Receipts", new[] { "CustomerId" });
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "UserId" });
            DropIndex("dbo.BuyingProducts", new[] { "ProductId" });
            DropIndex("dbo.BuyingProducts", new[] { "UserId" });
            DropTable("dbo.WareHouses");
            DropTable("dbo.SaleProducts");
            DropTable("dbo.Receipts");
            DropTable("dbo.Customers");
            DropTable("dbo.Products");
            DropTable("dbo.BuyingProducts");
        }
    }
}
