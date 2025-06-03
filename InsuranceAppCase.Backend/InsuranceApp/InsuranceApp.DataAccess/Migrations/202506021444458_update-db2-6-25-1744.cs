namespace InsuranceApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb26251744 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HealthPolicies", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Offers", "HealthPolicyId", "dbo.HealthPolicies");
            DropForeignKey("dbo.Rules", "OfferId", "dbo.Offers");
            DropIndex("dbo.HealthPolicies", new[] { "CustomerId" });
            DropIndex("dbo.Offers", new[] { "HealthPolicyId" });
            DropIndex("dbo.Rules", new[] { "OfferId" });
            CreateTable(
                "dbo.OfferHealthPolicies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfferId = c.Int(nullable: false),
                        HealthPolicyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HealthPolicies", t => t.HealthPolicyId, cascadeDelete: true)
                .ForeignKey("dbo.Offers", t => t.OfferId, cascadeDelete: true)
                .Index(t => t.OfferId)
                .Index(t => t.HealthPolicyId);
            
            AddColumn("dbo.Offers", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Rules", "MinAge", c => c.Int(nullable: false));
            AddColumn("dbo.Rules", "MaxAge", c => c.Int(nullable: false));
            AddColumn("dbo.Rules", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Rules", "HealthPolicyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Offers", "CustomerId");
            CreateIndex("dbo.Rules", "HealthPolicyId");
            AddForeignKey("dbo.Offers", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Rules", "HealthPolicyId", "dbo.HealthPolicies", "Id", cascadeDelete: true);
            DropColumn("dbo.HealthPolicies", "CustomerId");
            DropColumn("dbo.Offers", "HealthPolicyId");
            DropColumn("dbo.Rules", "Name");
            DropColumn("dbo.Rules", "Description");
            DropColumn("dbo.Rules", "OfferId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rules", "OfferId", c => c.Int(nullable: false));
            AddColumn("dbo.Rules", "Description", c => c.String());
            AddColumn("dbo.Rules", "Name", c => c.String());
            AddColumn("dbo.Offers", "HealthPolicyId", c => c.Int(nullable: false));
            AddColumn("dbo.HealthPolicies", "CustomerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OfferHealthPolicies", "OfferId", "dbo.Offers");
            DropForeignKey("dbo.Rules", "HealthPolicyId", "dbo.HealthPolicies");
            DropForeignKey("dbo.OfferHealthPolicies", "HealthPolicyId", "dbo.HealthPolicies");
            DropForeignKey("dbo.Offers", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Rules", new[] { "HealthPolicyId" });
            DropIndex("dbo.OfferHealthPolicies", new[] { "HealthPolicyId" });
            DropIndex("dbo.OfferHealthPolicies", new[] { "OfferId" });
            DropIndex("dbo.Offers", new[] { "CustomerId" });
            DropColumn("dbo.Rules", "HealthPolicyId");
            DropColumn("dbo.Rules", "Gender");
            DropColumn("dbo.Rules", "MaxAge");
            DropColumn("dbo.Rules", "MinAge");
            DropColumn("dbo.Offers", "CustomerId");
            DropTable("dbo.OfferHealthPolicies");
            CreateIndex("dbo.Rules", "OfferId");
            CreateIndex("dbo.Offers", "HealthPolicyId");
            CreateIndex("dbo.HealthPolicies", "CustomerId");
            AddForeignKey("dbo.Rules", "OfferId", "dbo.Offers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Offers", "HealthPolicyId", "dbo.HealthPolicies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.HealthPolicies", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
