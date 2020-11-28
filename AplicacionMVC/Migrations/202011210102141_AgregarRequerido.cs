namespace AplicacionMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarRequerido : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Personas", "Nombre", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Personas", "Nombre", c => c.String(maxLength: 50));
        }
    }
}
