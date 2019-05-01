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
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        private virtualrobot_playerEntities1 db = new virtualrobot_playerEntities1();

        [HttpPost]
        [Route("authenticate")]
        [ResponseType(typeof(Jugador))]
        public IHttpActionResult PostLogin(LoginRequest loginrequest)
        {

            Jugador jugador = db.Jugador.SqlQuery("SELECT * FROM virtualrobot_player.Jugador where nickname = @p0", loginrequest.Username).FirstOrDefault();

            if (jugador == null)
            {
                return NotFound();
            }
            if (jugador.passwordHash == loginrequest.Password)
            {
                jugador.CadenaToken = TokenGenerator.GenerateTokenJwt(loginrequest.Username);
                return Ok(jugador);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("register")]
        [ResponseType(typeof(Jugador))]
        public IHttpActionResult PostRegister(Jugador jugador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jugador.Add(jugador);
            db.SaveChanges();

            return Ok();
        }

    }
}