namespace AplicacionMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBIinicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        PersonaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 50),
                        Apellido = c.String(maxLength: 50),
                        Telefono = c.String(maxLength: 20),
                        Direccion = c.String(maxLength: 120),
                        Correo = c.String(),
                        Documento = c.String(nullable: false, maxLength: 20),
                        TipoDocumentoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PersonaId)
                .ForeignKey("dbo.TiposDocumento", t => t.TipoDocumentoId, cascadeDelete: true)
                .Index(t => t.TipoDocumentoId);
            
            CreateTable(
                "dbo.TiposDocumento",
                c => new
                    {
                        TipoDocumentoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TipoDocumentoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personas", "TipoDocumentoId", "dbo.TiposDocumento");
            DropIndex("dbo.Personas", new[] { "TipoDocumentoId" });
            DropTable("dbo.TiposDocumento");
            DropTable("dbo.Personas");
        }
    }
}
