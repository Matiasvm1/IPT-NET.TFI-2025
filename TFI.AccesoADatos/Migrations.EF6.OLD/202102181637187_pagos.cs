namespace TFI.AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pagos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pagos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaHora = c.DateTime(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Ventas", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ventas", "Total", c => c.Double(nullable: false));
            DropTable("dbo.Pagos");
        }
    }
}
