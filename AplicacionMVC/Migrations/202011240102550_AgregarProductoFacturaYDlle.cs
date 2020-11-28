namespace AplicacionMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarProductoFacturaYDlle : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DetalleFactura",
                c => new
                    {
                        DetalleFacturaId = c.Int(nullable: false, identity: true),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cantidad = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Factura_FacturaId = c.Int(),
                    })
                .PrimaryKey(t => t.DetalleFacturaId)
                .ForeignKey("dbo.Facturas", t => t.Factura_FacturaId)
                .ForeignKey("dbo.Productos", t => t.ProductoId)
                .Index(t => t.ProductoId)
                .Index(t => t.Factura_FacturaId);
            
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        FacturaId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        PersonaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FacturaId)
                .ForeignKey("dbo.Personas", t => t.PersonaId)
                .Index(t => t.PersonaId);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        ProductoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 50),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleFactura", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.Facturas", "PersonaId", "dbo.Personas");
            DropForeignKey("dbo.DetalleFactura", "Factura_FacturaId", "dbo.Facturas");
            DropIndex("dbo.Facturas", new[] { "PersonaId" });
            DropIndex("dbo.DetalleFactura", new[] { "Factura_FacturaId" });
            DropIndex("dbo.DetalleFactura", new[] { "ProductoId" });
            DropTable("dbo.Productos");
            DropTable("dbo.Facturas");
            DropTable("dbo.DetalleFactura");
        }
    }
}
