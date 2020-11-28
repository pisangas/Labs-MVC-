namespace AplicacionMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarOnModelCreating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Personas", "TipoDocumentoId", "dbo.TiposDocumento");
            AddForeignKey("dbo.Personas", "TipoDocumentoId", "dbo.TiposDocumento", "TipoDocumentoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personas", "TipoDocumentoId", "dbo.TiposDocumento");
            AddForeignKey("dbo.Personas", "TipoDocumentoId", "dbo.TiposDocumento", "TipoDocumentoId", cascadeDelete: true);
        }
    }
}
