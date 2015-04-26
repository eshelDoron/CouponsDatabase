namespace databases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.orders", "coupon_name", "dbo.coupons");
            DropIndex("dbo.orders", new[] { "coupon_name" });
            CreateTable(
                "dbo.ordercoupons",
                c => new
                    {
                        order_serialKey = c.String(nullable: false, maxLength: 128),
                        coupon_name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.order_serialKey, t.coupon_name })
                .ForeignKey("dbo.orders", t => t.order_serialKey, cascadeDelete: true)
                .ForeignKey("dbo.coupons", t => t.coupon_name, cascadeDelete: true)
                .Index(t => t.order_serialKey)
                .Index(t => t.coupon_name);
            
            DropColumn("dbo.orders", "coupon_name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.orders", "coupon_name", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ordercoupons", "coupon_name", "dbo.coupons");
            DropForeignKey("dbo.ordercoupons", "order_serialKey", "dbo.orders");
            DropIndex("dbo.ordercoupons", new[] { "coupon_name" });
            DropIndex("dbo.ordercoupons", new[] { "order_serialKey" });
            DropTable("dbo.ordercoupons");
            CreateIndex("dbo.orders", "coupon_name");
            AddForeignKey("dbo.orders", "coupon_name", "dbo.coupons", "name");
        }
    }
}
