namespace TFI.AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commitinicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empleados",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Legajo = c.Int(nullable: false),
                        Contraseña = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaHora = c.DateTime(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Indumentarias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Precio = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Indumentaria_Id = c.Int(),
                        Talle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Indumentarias", t => t.Indumentaria_Id)
                .ForeignKey("dbo.Talles", t => t.Talle_Id)
                .Index(t => t.Indumentaria_Id)
                .Index(t => t.Talle_Id);
            
            CreateTable(
                "dbo.Talles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LineasDeVenta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        Indumentaria_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Indumentarias", t => t.Indumentaria_Id)
                .Index(t => t.Indumentaria_Id);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaHora = c.DateTime(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LineasDeVenta", "Indumentaria_Id", "dbo.Indumentarias");
            DropForeignKey("dbo.Stock", "Talle_Id", "dbo.Talles");
            DropForeignKey("dbo.Stock", "Indumentaria_Id", "dbo.Indumentarias");
            DropIndex("dbo.LineasDeVenta", new[] { "Indumentaria_Id" });
            DropIndex("dbo.Stock", new[] { "Talle_Id" });
            DropIndex("dbo.Stock", new[] { "Indumentaria_Id" });
            DropTable("dbo.Ventas");
            DropTable("dbo.LineasDeVenta");
            DropTable("dbo.Talles");
            DropTable("dbo.Stock");
            DropTable("dbo.Indumentarias");
            DropTable("dbo.Facturas");
            DropTable("dbo.Empleados");
        }
    }
}
