namespace FindNearestPlaces.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGeographyPlace : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeographyPlaces",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Location = c.Geography(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GeographyPlaces");
        }
    }
}
