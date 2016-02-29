namespace WebAPI.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomSizes", "Size", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomSizes", "Size", c => c.Int(nullable: false));
        }
    }
}
