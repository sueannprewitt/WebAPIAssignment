namespace WebAPIAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNbr = c.String(nullable: false, maxLength: 20),
                        DateReceived = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.OrderNbr, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Orders", new[] { "OrderNbr" });
            DropTable("dbo.Orders");
        }
    }
}
