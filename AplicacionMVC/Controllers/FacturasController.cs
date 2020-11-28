using AplicacionMVC.Models;
using AplicacionMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionMVC.Controllers
{
    public class FacturasController : Controller
    {
        private Contexto db = new Contexto();

        #region [Acciones para Factura]
        // GET: Facturas
        public ActionResult Nuevafactura()
        {
            FacturaViewModel facturaViewModel = new FacturaViewModel() { Productos = new List<ProductoViewModel>() };
            ViewBag.ClienteId = new SelectList(db.Personas.OrderBy(p => p.Nombre).ThenBy(a => a.Apellido), "PersonaId", "NombreCompleto");

            Session["FacturaViewModel"] = facturaViewModel;
            return View(facturaViewModel);
        }
        #endregion

        #region [Acciones para Producto]
        public ActionResult AgregarProducto(int? ClienteId)
        {
            Session["ClienteId"] = ClienteId;
            ProductoViewModel productoViewModel = new ProductoViewModel();

            ViewBag.ProductoId = new SelectList(db.Productos.OrderBy(p => p.Descripcion), "ProductoId", "Descripcion");

            return View(productoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarProducto(ProductoViewModel productoDetalleFactura)
        {
            if (ModelState.IsValid)
            {
                FacturaViewModel facturaViewModel = Session["FacturaViewModel"] as FacturaViewModel;
                if (!facturaViewModel.Productos.Exists(p => p.ProductoId == productoDetalleFactura.ProductoId))
                {
                    var producto = db.Productos.Find(productoDetalleFactura.ProductoId);

                    productoDetalleFactura.Descripcion = producto.Descripcion;
                    productoDetalleFactura.Precio = producto.Precio;

                    facturaViewModel.Productos.Add(productoDetalleFactura);
                }
                else
                {
                    facturaViewModel.Productos.Find(p => p.ProductoId == productoDetalleFactura.ProductoId).Cantidad += productoDetalleFactura.Cantidad;
                }

                if (Session["ClienteId"] != null)
                {
                    ViewBag.ClienteId = new SelectList(db.Personas.OrderBy(p => p.Nombre).ThenBy(a => a.Apellido), "PersonaId", "NombreCompleto", Session["ClienteId"]);
                }
                else
                {
                    ViewBag.ClienteId = new SelectList(db.Personas.OrderBy(p => p.Nombre).ThenBy(a => a.Apellido), "PersonaId", "NombreCompleto");
                }


                return View("Nuevafactura", facturaViewModel);
            }

            ViewBag.ProductoId = new SelectList(db.Productos.OrderBy(p => p.Descripcion), "ProductoId", "Descripcion");

            return View(productoDetalleFactura);


        }
        public ActionResult CancelarAgregarProducto()
        {
            FacturaViewModel facturaViewModel = Session["FacturaViewModel"] as FacturaViewModel;

            if (Session["ClienteId"] != null)
            {
                ViewBag.ClienteId = new SelectList(db.Personas.OrderBy(p => p.Nombre).ThenBy(a => a.Apellido), "PersonaId", "NombreCompleto", Session["ClienteId"]);
            }
            else
            {
                ViewBag.ClienteId = new SelectList(db.Personas.OrderBy(p => p.Nombre).ThenBy(a => a.Apellido), "PersonaId", "NombreCompleto");
            }

            return View("NuevaFactura", facturaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevaFactura(FacturaViewModel facturaViewModel)
        {
            if (ModelState.IsValid)
            {
                var ingresarfacturaViewModel = Session["facturaViewModel"] as FacturaViewModel;

                if (ingresarfacturaViewModel.Productos == null || ingresarfacturaViewModel.Productos.Count()==0)
                {
                    ViewBag.Error = "Debe ingresar Productos";

                    ViewBag.ClienteId = new SelectList(db.Personas.OrderBy(p => p.Nombre).ThenBy(a => a.Apellido), "PersonaId", "NombreCompleto");

                    return View(facturaViewModel);
                }

                using (var transaccion = db.Database.BeginTransaction())
                {
                    try
                    {
                        var factura = new Factura
                        {
                            PersonaId = facturaViewModel.ClienteId,
                            Fecha = DateTime.Now,
                            DetalleFacturas = ingresarfacturaViewModel.Productos.Select(p => new DetalleFactura { ProductoId = p.ProductoId, Precio = p.Precio, Cantidad = p.Cantidad }).ToList()
                        };

                        db.Facturas.Add(factura);
                        db.SaveChanges();

                        transaccion.Commit();

                        ViewBag.Mensaje = $"La factura Nro. {factura.FacturaId} fue ingresada exitosamente";

                        facturaViewModel = new FacturaViewModel() { Productos = new List<ProductoViewModel>() };
                        ViewBag.ClienteId = new SelectList(db.Personas.OrderBy(p => p.Nombre).ThenBy(a => a.Apellido), "PersonaId", "NombreCompleto");

                        Session["FacturaViewModel"] = facturaViewModel;
                        return View(facturaViewModel);

                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        ViewBag.Error = $"Error: {ex.Message}";

                        ViewBag.ClienteId = new SelectList(db.Personas.OrderBy(p => p.Nombre).ThenBy(a => a.Apellido), "PersonaId", "NombreCompleto");

                        return View(facturaViewModel);
                        
                    }
                }


            }
            
            
            
            ViewBag.ClienteId = new SelectList(db.Personas.OrderBy(p => p.Nombre).ThenBy(a => a.Apellido), "PersonaId", "NombreCompleto");


            return View(facturaViewModel);
        }


        #endregion

        #region [Sobrecarga]

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        } 
        #endregion


    }
}