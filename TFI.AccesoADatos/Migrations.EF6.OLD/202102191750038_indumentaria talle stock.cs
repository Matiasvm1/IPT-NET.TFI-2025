namespace TFI.AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class indumentariatallestock : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stock", "Talle_Id", "dbo.Talles");
            DropIndex("dbo.Stock", new[] { "Talle_Id" });
            AddColumn("dbo.Indumentarias", "Talle_Id", c => c.Int());
            CreateIndex("dbo.Indumentarias", "Talle_Id");
            AddForeignKey("dbo.Indumentarias", "Talle_Id", "dbo.Talles", "Id");
            DropColumn("dbo.Stock", "Talle_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stock", "Talle_Id", c => c.Int());
            DropForeignKey("dbo.Indumentarias", "Talle_Id", "dbo.Talles");
            DropIndex("dbo.Indumentarias", new[] { "Talle_Id" });
            DropColumn("dbo.Indumentarias", "Talle_Id");
            CreateIndex("dbo.Stock", "Talle_Id");
            AddForeignKey("dbo.Stock", "Talle_Id", "dbo.Talles", "Id");
        }
    }
}
