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
using VirtualBackend.Models;

namespace VirtualBackend.Controllers
{
    public class JugadorsController : ApiController
    {
        private virtualrobot_playerEntities1 db = new virtualrobot_playerEntities1();

        // GET: api/Jugadors/5
        [ResponseType(typeof(Jugador))]
        public IHttpActionResult GetJugador(int id)
        {
            Jugador jugador = db.Jugador.Find(id);
            if (jugador == null)
            {
                return NotFound();
            }

            return Ok(jugador);
        }

        // PUT: api/Jugadors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJugador(int id, Jugador jugador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jugador.idJugador)
            {
                return BadRequest();
            }

            db.Entry(jugador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JugadorExists(id))
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

        // DELETE: api/Jugadors/5
        [ResponseType(typeof(Jugador))]
        public IHttpActionResult DeleteJugador(int id)
        {
            Jugador jugador = db.Jugador.Find(id);
            if (jugador == null)
            {
                return NotFound();
            }

            db.Jugador.Remove(jugador);
            db.SaveChanges();

            return Ok(jugador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JugadorExists(int id)
        {
            return db.Jugador.Count(e => e.idJugador == id) > 0;
        }
    }
}