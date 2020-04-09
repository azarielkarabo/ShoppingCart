namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedTime = c.DateTime(),
                        LastUpdatedTimestamp = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImagePath = c.String(),
                        Price = c.Double(),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedTime = c.DateTime(),
                        LastUpdatedTimestamp = c.DateTime(),
                        Category_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedTime = c.DateTime(),
                        LastUpdatedTimestamp = c.DateTime(),
                        Product_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropIndex("dbo.CartItems", new[] { "Product_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropTable("dbo.CartItems");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
