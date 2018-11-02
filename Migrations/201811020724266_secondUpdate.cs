namespace ImageEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Photos", "ImagePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "ImagePath", c => c.Byte(nullable: false));
        }
    }
}
