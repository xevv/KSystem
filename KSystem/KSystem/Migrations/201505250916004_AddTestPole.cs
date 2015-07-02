namespace KSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestPole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TestInt", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TestInt");
        }
    }
}
