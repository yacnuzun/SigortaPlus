namespace InsuranceApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstIntialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HealthPolicies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PolicyNumber = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfferCode = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValidUntil = c.DateTime(nullable: false),
                        HealthPolicyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthPolicies", t => t.HealthPolicyId, cascadeDelete: true)
                .Index(t => t.HealthPolicyId);
            
            CreateTable(
                "dbo.Rules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        OfferId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Offers", t => t.OfferId, cascadeDelete: true)
                .Index(t => t.OfferId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rules", "OfferId", "dbo.Offers");
            DropForeignKey("dbo.Offers", "HealthPolicyId", "dbo.HealthPolicies");
            DropForeignKey("dbo.HealthPolicies", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Rules", new[] { "OfferId" });
            DropIndex("dbo.Offers", new[] { "HealthPolicyId" });
            DropIndex("dbo.HealthPolicies", new[] { "CustomerId" });
            DropTable("dbo.Rules");
            DropTable("dbo.Offers");
            DropTable("dbo.HealthPolicies");
            DropTable("dbo.Customers");
        }
    }
}
