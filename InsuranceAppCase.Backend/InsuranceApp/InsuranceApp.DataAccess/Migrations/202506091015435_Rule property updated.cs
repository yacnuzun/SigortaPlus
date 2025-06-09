namespace InsuranceApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rulepropertyupdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rules", "Gender", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rules", "Gender", c => c.Int(nullable: false));
        }
    }
}
