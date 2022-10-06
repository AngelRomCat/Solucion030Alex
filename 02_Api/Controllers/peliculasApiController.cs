using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using _04_Data.Datos;

namespace _02_Api.Controllers
{
    public class peliculasApiController : ApiController
    {
        private MarvelDbContext db = new MarvelDbContext();

        // GET: api/peliculasApi
        public IQueryable<pelicula> Getpelicula()
        {
            return db.pelicula;
        }

        // GET: api/peliculasApi/5
        [ResponseType(typeof(pelicula))]
        public IHttpActionResult Getpelicula(int id)
        {
            pelicula pelicula = db.pelicula.Find(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return Ok(pelicula);
        }
        // GET: api/peliculas/5
        [ResponseType(typeof(pelicula))]
        public IHttpActionResult Getpelicula(int? id, int? siguiente)
        {
            pelicula peliculaTabla = null;
            if (siguiente == null)
            {
                peliculaTabla = db.pelicula.Where(x => x.id == id.Value).FirstOrDefault();
            }
            else
            {
                if (siguiente.Value == 1)
                {
                    peliculaTabla = db.pelicula.Where(x => x.id > id.Value).FirstOrDefault();
                }
                else
                {
                    IList<pelicula> peliculaTablas = db.pelicula.Where(x => x.id < id.Value).ToList();
                    if (peliculaTablas != null && peliculaTablas.Count() > 0)
                    {
                        int? idpelicula = peliculaTablas.Max(x => x.id);
                        peliculaTabla = db.pelicula.Where(x => x.id == idpelicula.Value).FirstOrDefault();
                    }
                }
            }
            if (peliculaTabla == null)
            {
                peliculaTabla = db.pelicula.Where(x => x.id == id.Value).FirstOrDefault();
            }
            pelicula pelicula = new pelicula();
            pelicula.id = peliculaTabla.id;
            pelicula.titulo = peliculaTabla.titulo;
            pelicula.añolanzamiento = peliculaTabla.añolanzamiento;
            pelicula.sinopsis = peliculaTabla.sinopsis;
            pelicula.premios = peliculaTabla.premios;
            pelicula.duracion = peliculaTabla.duracion;
            pelicula.clasificacion = peliculaTabla.clasificacion;
            pelicula.presupuesto = peliculaTabla.presupuesto;
            pelicula.recaudacion = peliculaTabla.recaudacion;

            return Ok(pelicula);
        }


        // PUT: api/peliculasApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpelicula(int id, pelicula pelicula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pelicula.id)
            {
                return BadRequest();
            }

            db.Entry(pelicula).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!peliculaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/peliculasApi
        [ResponseType(typeof(pelicula))]
        public IHttpActionResult Postpelicula(pelicula pelicula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.pelicula.Add(pelicula);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pelicula.id }, pelicula);
        }

        // DELETE: api/peliculasApi/5
        [ResponseType(typeof(pelicula))]
        public IHttpActionResult Deletepelicula(int id)
        {
            pelicula pelicula = db.pelicula.Find(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            db.pelicula.Remove(pelicula);
            db.SaveChanges();

            return Ok(pelicula);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool peliculaExists(int id)
        {
            return db.pelicula.Count(e => e.id == id) > 0;
        }
    }
}