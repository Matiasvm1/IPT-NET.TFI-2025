namespace TFI.AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cantidadstockpublic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stock", "Cantidad", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stock", "Cantidad");
        }
    }
}
