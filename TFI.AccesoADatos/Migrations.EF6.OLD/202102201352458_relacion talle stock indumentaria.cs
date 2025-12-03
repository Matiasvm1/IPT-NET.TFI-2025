namespace TFI.AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relaciontallestockindumentaria : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Indumentarias", "Talle_Id", "dbo.Talles");
            DropIndex("dbo.Indumentarias", new[] { "Talle_Id" });
            AddColumn("dbo.Pagos", "Factura_Id", c => c.Int());
            AddColumn("dbo.Stock", "Talle_Id", c => c.Int());
            AddColumn("dbo.Ventas", "Pago_Id", c => c.Int());
            CreateIndex("dbo.Pagos", "Factura_Id");
            CreateIndex("dbo.Stock", "Talle_Id");
            CreateIndex("dbo.Ventas", "Pago_Id");
            AddForeignKey("dbo.Pagos", "Factura_Id", "dbo.Facturas", "Id");
            AddForeignKey("dbo.Stock", "Talle_Id", "dbo.Talles", "Id");
            AddForeignKey("dbo.Ventas", "Pago_Id", "dbo.Pagos", "Id");
            DropColumn("dbo.Indumentarias", "Talle_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Indumentarias", "Talle_Id", c => c.Int());
            DropForeignKey("dbo.Ventas", "Pago_Id", "dbo.Pagos");
            DropForeignKey("dbo.Stock", "Talle_Id", "dbo.Talles");
            DropForeignKey("dbo.Pagos", "Factura_Id", "dbo.Facturas");
            DropIndex("dbo.Ventas", new[] { "Pago_Id" });
            DropIndex("dbo.Stock", new[] { "Talle_Id" });
            DropIndex("dbo.Pagos", new[] { "Factura_Id" });
            DropColumn("dbo.Ventas", "Pago_Id");
            DropColumn("dbo.Stock", "Talle_Id");
            DropColumn("dbo.Pagos", "Factura_Id");
            CreateIndex("dbo.Indumentarias", "Talle_Id");
            AddForeignKey("dbo.Indumentarias", "Talle_Id", "dbo.Talles", "Id");
        }
    }
}
