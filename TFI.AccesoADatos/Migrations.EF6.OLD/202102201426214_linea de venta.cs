namespace TFI.AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lineadeventa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LineasDeVenta", "Venta_Id", c => c.Int());
            CreateIndex("dbo.LineasDeVenta", "Venta_Id");
            AddForeignKey("dbo.LineasDeVenta", "Venta_Id", "dbo.Ventas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LineasDeVenta", "Venta_Id", "dbo.Ventas");
            DropIndex("dbo.LineasDeVenta", new[] { "Venta_Id" });
            DropColumn("dbo.LineasDeVenta", "Venta_Id");
        }
    }
}
