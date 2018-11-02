namespace ImageEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourthUpdate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Photos", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "UserId", c => c.Int(nullable: false));
        }
    }
}
