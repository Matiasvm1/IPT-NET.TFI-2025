namespace TFI.AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codigoenindumentaria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indumentarias", "Codigo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Indumentarias", "Codigo");
        }
    }
}
