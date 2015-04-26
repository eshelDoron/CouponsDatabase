namespace databases.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.buisCouponReports",
                c => new
                    {
                        subject = c.String(nullable: false, maxLength: 128),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.subject, t.date });
            
            CreateTable(
                "dbo.businesses",
                c => new
                    {
                        businessName = c.String(nullable: false, maxLength: 128),
                        address = c.String(),
                        city = c.String(),
                        description = c.String(),
                        category_categoryName = c.String(maxLength: 128),
                        businessOwner_userName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.businessName)
                .ForeignKey("dbo.categories", t => t.category_categoryName)
                .ForeignKey("dbo.users", t => t.businessOwner_userName)
                .Index(t => t.category_categoryName)
                .Index(t => t.businessOwner_userName);
            
            CreateTable(
                "dbo.buisRateReports",
                c => new
                    {
                        subject = c.String(nullable: false, maxLength: 128),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.subject, t.date });
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        userName = c.String(nullable: false, maxLength: 128),
                        mail = c.String(),
                        tel = c.String(),
                        strongPassword = c.String(),
                        connected = c.Boolean(nullable: false),
                        age = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.userName);
            
            CreateTable(
                "dbo.alerts",
                c => new
                    {
                        subject = c.String(nullable: false, maxLength: 128),
                        date = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.subject, t.date });
            
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        categoryName = c.String(nullable: false, maxLength: 128),
                        coupon_name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.categoryName)
                .ForeignKey("dbo.coupons", t => t.coupon_name)
                .Index(t => t.coupon_name);
            
            CreateTable(
                "dbo.couponRequests",
                c => new
                    {
                        description = c.String(nullable: false, maxLength: 128),
                        coupons_name = c.String(maxLength: 128),
                        manager_userName = c.String(maxLength: 128),
                        customer_userName = c.String(maxLength: 128),
                        businessOwner_userName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.description)
                .ForeignKey("dbo.coupons", t => t.coupons_name)
                .ForeignKey("dbo.users", t => t.manager_userName)
                .ForeignKey("dbo.users", t => t.customer_userName)
                .ForeignKey("dbo.users", t => t.businessOwner_userName)
                .Index(t => t.coupons_name)
                .Index(t => t.manager_userName)
                .Index(t => t.customer_userName)
                .Index(t => t.businessOwner_userName);
            
            CreateTable(
                "dbo.coupons",
                c => new
                    {
                        name = c.String(nullable: false, maxLength: 128),
                        description = c.String(),
                        originalPrice = c.Double(nullable: false),
                        discountPrice = c.Double(nullable: false),
                        rate = c.Double(nullable: false),
                        lastDate = c.DateTime(nullable: false),
                        businessName = c.String(maxLength: 128),
                        manager_userName = c.String(maxLength: 128),
                        categoryAlert_subject = c.String(maxLength: 128),
                        categoryAlert_date = c.DateTime(),
                    })
                .PrimaryKey(t => t.name)
                .ForeignKey("dbo.businesses", t => t.businessName)
                .ForeignKey("dbo.users", t => t.manager_userName)
                .ForeignKey("dbo.alerts", t => new { t.categoryAlert_subject, t.categoryAlert_date })
                .Index(t => t.businessName)
                .Index(t => t.manager_userName)
                .Index(t => new { t.categoryAlert_subject, t.categoryAlert_date });
            
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        serialKey = c.String(nullable: false, maxLength: 128),
                        paymentDetails = c.String(),
                        dateTime = c.DateTime(nullable: false),
                        used = c.Boolean(nullable: false),
                        name = c.Int(nullable: false),
                        date = c.Int(nullable: false),
                        business_businessName = c.String(maxLength: 128),
                        customer_userName = c.String(maxLength: 128),
                        expirationAlert_subject = c.String(maxLength: 128),
                        expirationAlert_date = c.DateTime(),
                        userCouponReport_subject = c.String(maxLength: 128),
                        userCouponReport_date = c.DateTime(),
                        coupon_name = c.String(maxLength: 128),
                        rateCoupon_rate = c.Int(),
                    })
                .PrimaryKey(t => t.serialKey)
                .ForeignKey("dbo.businesses", t => t.business_businessName)
                .ForeignKey("dbo.users", t => t.customer_userName)
                .ForeignKey("dbo.alerts", t => new { t.expirationAlert_subject, t.expirationAlert_date })
                .ForeignKey("dbo.userCouponReports", t => new { t.userCouponReport_subject, t.userCouponReport_date })
                .ForeignKey("dbo.coupons", t => t.coupon_name)
                .ForeignKey("dbo.rateCoupons", t => t.rateCoupon_rate)
                .Index(t => t.business_businessName)
                .Index(t => t.customer_userName)
                .Index(t => new { t.expirationAlert_subject, t.expirationAlert_date })
                .Index(t => new { t.userCouponReport_subject, t.userCouponReport_date })
                .Index(t => t.coupon_name)
                .Index(t => t.rateCoupon_rate);
            
            CreateTable(
                "dbo.userCouponReports",
                c => new
                    {
                        subject = c.String(nullable: false, maxLength: 128),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.subject, t.date });
            
            CreateTable(
                "dbo.rateCoupons",
                c => new
                    {
                        rate = c.Int(nullable: false, identity: true),
                        serialKey = c.String(),
                        customer_userName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.rate)
                .ForeignKey("dbo.users", t => t.customer_userName)
                .Index(t => t.customer_userName);
            
            CreateTable(
                "dbo.businessbuisCouponReports",
                c => new
                    {
                        business_businessName = c.String(nullable: false, maxLength: 128),
                        buisCouponReport_subject = c.String(nullable: false, maxLength: 128),
                        buisCouponReport_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.business_businessName, t.buisCouponReport_subject, t.buisCouponReport_date })
                .ForeignKey("dbo.businesses", t => t.business_businessName, cascadeDelete: true)
                .ForeignKey("dbo.buisCouponReports", t => new { t.buisCouponReport_subject, t.buisCouponReport_date }, cascadeDelete: true)
                .Index(t => t.business_businessName)
                .Index(t => new { t.buisCouponReport_subject, t.buisCouponReport_date });
            
            CreateTable(
                "dbo.buisRateReportbusinesses",
                c => new
                    {
                        buisRateReport_subject = c.String(nullable: false, maxLength: 128),
                        buisRateReport_date = c.DateTime(nullable: false),
                        business_businessName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.buisRateReport_subject, t.buisRateReport_date, t.business_businessName })
                .ForeignKey("dbo.buisRateReports", t => new { t.buisRateReport_subject, t.buisRateReport_date }, cascadeDelete: true)
                .ForeignKey("dbo.businesses", t => t.business_businessName, cascadeDelete: true)
                .Index(t => new { t.buisRateReport_subject, t.buisRateReport_date })
                .Index(t => t.business_businessName);
            
            CreateTable(
                "dbo.useralerts",
                c => new
                    {
                        user_userName = c.String(nullable: false, maxLength: 128),
                        alert_subject = c.String(nullable: false, maxLength: 128),
                        alert_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.user_userName, t.alert_subject, t.alert_date })
                .ForeignKey("dbo.users", t => t.user_userName, cascadeDelete: true)
                .ForeignKey("dbo.alerts", t => new { t.alert_subject, t.alert_date }, cascadeDelete: true)
                .Index(t => t.user_userName)
                .Index(t => new { t.alert_subject, t.alert_date });
            
            CreateTable(
                "dbo.categorycustomers",
                c => new
                    {
                        category_categoryName = c.String(nullable: false, maxLength: 128),
                        customer_userName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.category_categoryName, t.customer_userName })
                .ForeignKey("dbo.categories", t => t.category_categoryName, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.customer_userName, cascadeDelete: true)
                .Index(t => t.category_categoryName)
                .Index(t => t.customer_userName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.orders", "rateCoupon_rate", "dbo.rateCoupons");
            DropForeignKey("dbo.rateCoupons", "customer_userName", "dbo.users");
            DropForeignKey("dbo.couponRequests", "businessOwner_userName", "dbo.users");
            DropForeignKey("dbo.businesses", "businessOwner_userName", "dbo.users");
            DropForeignKey("dbo.coupons", new[] { "categoryAlert_subject", "categoryAlert_date" }, "dbo.alerts");
            DropForeignKey("dbo.couponRequests", "customer_userName", "dbo.users");
            DropForeignKey("dbo.orders", "coupon_name", "dbo.coupons");
            DropForeignKey("dbo.orders", new[] { "userCouponReport_subject", "userCouponReport_date" }, "dbo.userCouponReports");
            DropForeignKey("dbo.orders", new[] { "expirationAlert_subject", "expirationAlert_date" }, "dbo.alerts");
            DropForeignKey("dbo.orders", "customer_userName", "dbo.users");
            DropForeignKey("dbo.orders", "business_businessName", "dbo.businesses");
            DropForeignKey("dbo.coupons", "manager_userName", "dbo.users");
            DropForeignKey("dbo.couponRequests", "manager_userName", "dbo.users");
            DropForeignKey("dbo.couponRequests", "coupons_name", "dbo.coupons");
            DropForeignKey("dbo.categories", "coupon_name", "dbo.coupons");
            DropForeignKey("dbo.coupons", "businessName", "dbo.businesses");
            DropForeignKey("dbo.categorycustomers", "customer_userName", "dbo.users");
            DropForeignKey("dbo.categorycustomers", "category_categoryName", "dbo.categories");
            DropForeignKey("dbo.businesses", "category_categoryName", "dbo.categories");
            DropForeignKey("dbo.useralerts", new[] { "alert_subject", "alert_date" }, "dbo.alerts");
            DropForeignKey("dbo.useralerts", "user_userName", "dbo.users");
            DropForeignKey("dbo.buisRateReportbusinesses", "business_businessName", "dbo.businesses");
            DropForeignKey("dbo.buisRateReportbusinesses", new[] { "buisRateReport_subject", "buisRateReport_date" }, "dbo.buisRateReports");
            DropForeignKey("dbo.businessbuisCouponReports", new[] { "buisCouponReport_subject", "buisCouponReport_date" }, "dbo.buisCouponReports");
            DropForeignKey("dbo.businessbuisCouponReports", "business_businessName", "dbo.businesses");
            DropIndex("dbo.categorycustomers", new[] { "customer_userName" });
            DropIndex("dbo.categorycustomers", new[] { "category_categoryName" });
            DropIndex("dbo.useralerts", new[] { "alert_subject", "alert_date" });
            DropIndex("dbo.useralerts", new[] { "user_userName" });
            DropIndex("dbo.buisRateReportbusinesses", new[] { "business_businessName" });
            DropIndex("dbo.buisRateReportbusinesses", new[] { "buisRateReport_subject", "buisRateReport_date" });
            DropIndex("dbo.businessbuisCouponReports", new[] { "buisCouponReport_subject", "buisCouponReport_date" });
            DropIndex("dbo.businessbuisCouponReports", new[] { "business_businessName" });
            DropIndex("dbo.rateCoupons", new[] { "customer_userName" });
            DropIndex("dbo.orders", new[] { "rateCoupon_rate" });
            DropIndex("dbo.orders", new[] { "coupon_name" });
            DropIndex("dbo.orders", new[] { "userCouponReport_subject", "userCouponReport_date" });
            DropIndex("dbo.orders", new[] { "expirationAlert_subject", "expirationAlert_date" });
            DropIndex("dbo.orders", new[] { "customer_userName" });
            DropIndex("dbo.orders", new[] { "business_businessName" });
            DropIndex("dbo.coupons", new[] { "categoryAlert_subject", "categoryAlert_date" });
            DropIndex("dbo.coupons", new[] { "manager_userName" });
            DropIndex("dbo.coupons", new[] { "businessName" });
            DropIndex("dbo.couponRequests", new[] { "businessOwner_userName" });
            DropIndex("dbo.couponRequests", new[] { "customer_userName" });
            DropIndex("dbo.couponRequests", new[] { "manager_userName" });
            DropIndex("dbo.couponRequests", new[] { "coupons_name" });
            DropIndex("dbo.categories", new[] { "coupon_name" });
            DropIndex("dbo.businesses", new[] { "businessOwner_userName" });
            DropIndex("dbo.businesses", new[] { "category_categoryName" });
            DropTable("dbo.categorycustomers");
            DropTable("dbo.useralerts");
            DropTable("dbo.buisRateReportbusinesses");
            DropTable("dbo.businessbuisCouponReports");
            DropTable("dbo.rateCoupons");
            DropTable("dbo.userCouponReports");
            DropTable("dbo.orders");
            DropTable("dbo.coupons");
            DropTable("dbo.couponRequests");
            DropTable("dbo.categories");
            DropTable("dbo.alerts");
            DropTable("dbo.users");
            DropTable("dbo.buisRateReports");
            DropTable("dbo.businesses");
            DropTable("dbo.buisCouponReports");
        }
    }
}
