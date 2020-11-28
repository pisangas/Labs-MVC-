using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicacionMVC.Models;
using PagedList;

namespace AplicacionMVC.Controllers
{
    public class PersonasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Personas
        public ActionResult Index(string ordenamiento, string filtroActual, string filtro, int? pagina)
        {
            ViewBag.OrdenamientoActual = ordenamiento;
            
            ViewBag.NombreOrdenamiento = string.IsNullOrEmpty(ordenamiento) ? "nombre_desc" : "";

            if (filtro !=  null)
            {
                pagina = 1;
            }
            else
            {
                filtro = filtroActual;
            }

            ViewBag.FiltroActual = filtro;

            var personas = from p in db.Personas
                           select p;

            if (!string.IsNullOrEmpty(filtro))
            {
                personas = personas.Where(p => p.Nombre.Contains(filtro) || p.Apellido.Contains(filtro));
            }


            switch (ordenamiento)
            {
                case "nombre_desc":
                    personas = personas.OrderByDescending(p => p.Nombre);
                    break;
                default:
                    personas = personas.OrderBy(p => p.Nombre);
                    break;
            }

            int tamannopagina = 2;
            int numeroPagina = (pagina ?? 1); // si viene null le pone 1 sino respeta el valor


            //var personas = db.Personas.Include(p => p.TipoDocumento);
            return View(personas.ToPagedList(numeroPagina, tamannopagina));
        }

        // GET: Personas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = await db.Personas.FindAsync(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            ViewBag.TipoDocumentoId = new SelectList(db.TiposDocumento.OrderBy(t => t.Descripcion), "TipoDocumentoId", "Descripcion");
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PersonaId,Nombre,Apellido,Telefono,Direccion,Correo,Documento,TipoDocumentoId")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(persona);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.TipoDocumentoId = new SelectList(db.TiposDocumento.OrderBy(t => t.Descripcion), "TipoDocumentoId", "Descripcion", persona.TipoDocumentoId);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = await db.Personas.FindAsync(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoDocumentoId = new SelectList(db.TiposDocumento, "TipoDocumentoId", "Descripcion", persona.TipoDocumentoId);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PersonaId,Nombre,Apellido,Telefono,Direccion,Correo,Documento,TipoDocumentoId")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TipoDocumentoId = new SelectList(db.TiposDocumento, "TipoDocumentoId", "Descripcion", persona.TipoDocumentoId);
            return View(persona);
        }

        // GET: Personas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = await db.Personas.FindAsync(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Persona persona = await db.Personas.FindAsync(id);
            db.Personas.Remove(persona);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
