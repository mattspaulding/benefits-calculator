namespace AngularMaterialDotNet.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeRange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Name", c => c.String(nullable: false));
        }
    }
}
