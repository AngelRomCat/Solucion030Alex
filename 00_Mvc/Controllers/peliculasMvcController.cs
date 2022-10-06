using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _04_Data.Datos;

namespace _00_Mvc.Controllers
{
    public class peliculasMvcController : Controller
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: peliculasMvc
        public ActionResult Index()
        {
            var pelicula = db.pelicula.Include(p => p.actor).Include(p => p.companyia).Include(p => p.director).Include(p => p.genero);
            return View(pelicula.ToList());
        }

        // GET: peliculasMvc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.pelicula.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }


        // GET: peliculasMvc/Details/5
        public ActionResult DetailsAjax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.pelicula.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // GET: peliculasMvc/Create
        public ActionResult Create()
        {
            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal");
            ViewBag.id_companyia = new SelectList(db.companyia, "id", "nombre");
            ViewBag.id_director = new SelectList(db.director, "id", "nombre");
            ViewBag.id_genero = new SelectList(db.genero, "id", "nombre");
            return View();
        }

        // POST: peliculasMvc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_actor,id_director,id_genero,id_companyia,titulo,añolanzamiento,sinopsis,premios,duracion,clasificacion,presupuesto,recaudacion")] pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.pelicula.Add(pelicula);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal", pelicula.id_actor);
            ViewBag.id_companyia = new SelectList(db.companyia, "id", "nombre", pelicula.id_companyia);
            ViewBag.id_director = new SelectList(db.director, "id", "nombre", pelicula.id_director);
            ViewBag.id_genero = new SelectList(db.genero, "id", "nombre", pelicula.id_genero);
            return View(pelicula);
        }

        // GET: peliculasMvc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.pelicula.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal", pelicula.id_actor);
            ViewBag.id_companyia = new SelectList(db.companyia, "id", "nombre", pelicula.id_companyia);
            ViewBag.id_director = new SelectList(db.director, "id", "nombre", pelicula.id_director);
            ViewBag.id_genero = new SelectList(db.genero, "id", "nombre", pelicula.id_genero);
            return View(pelicula);
        }

        // POST: peliculasMvc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_actor,id_director,id_genero,id_companyia,titulo,añolanzamiento,sinopsis,premios,duracion,clasificacion,presupuesto,recaudacion")] pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pelicula).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_actor = new SelectList(db.actor, "id", "actor_principal", pelicula.id_actor);
            ViewBag.id_companyia = new SelectList(db.companyia, "id", "nombre", pelicula.id_companyia);
            ViewBag.id_director = new SelectList(db.director, "id", "nombre", pelicula.id_director);
            ViewBag.id_genero = new SelectList(db.genero, "id", "nombre", pelicula.id_genero);
            return View(pelicula);
        }

        // GET: peliculasMvc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pelicula pelicula = db.pelicula.Find(id);
            if (pelicula == null)
            {
                return HttpNotFound();
            }
            return View(pelicula);
        }

        // POST: peliculasMvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pelicula pelicula = db.pelicula.Find(id);
            db.pelicula.Remove(pelicula);
            db.SaveChanges();
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

        // POST: Ejercicio/_peliculaMvcOtraPartialView/5
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult _PeliculaMvcPartialView(pelicula pelicula)
        {
            return View("_PeliculaMvcPartialView", pelicula);
        }
    }
}
