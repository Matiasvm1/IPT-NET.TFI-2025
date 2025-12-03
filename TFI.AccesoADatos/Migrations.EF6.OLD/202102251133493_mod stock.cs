namespace TFI.AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modstock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stock", "CantidadMaxima", c => c.Int(nullable: false));
            AddColumn("dbo.Stock", "CantidadMinima", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stock", "CantidadMinima");
            DropColumn("dbo.Stock", "CantidadMaxima");
        }
    }
}
