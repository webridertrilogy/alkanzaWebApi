using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


using System.Web.Http.Description;
using alkanza.businesslogic;
using alkanza.businessobjects;
using alkanza.webapi.Models;

namespace alkanza.webapi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HistoricoController : ApiController
    {
        private alkanzawebapiContext db = new alkanzawebapiContext();

        // GET: api/Historico
        public IList<Historico> GetHistoricos()
        {
            return Manager.consultarHistoricos();
        }

        // GET: api/Historico/5
        [ResponseType(typeof(Historico))]
        public IHttpActionResult GetHistorico(Guid id)
        {
            Historico historico = new Historico();
            historico.id = id;
            historico = Manager.consultarHistorico(historico);
            if (historico == null)
            {
                return NotFound();
            }

            return Ok(historico);
        }

        // PUT: api/Historico/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHistorico(Guid id, Historico historico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != historico.id)
            {
                return BadRequest();
            }

            db.Entry(historico).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoricoExists(id))
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

        // POST: api/Historico
        [ResponseType(typeof(Historico))]
        public IHttpActionResult PostHistorico(Historico historico)
        {
            bool exitoso = Manager.crearHistorico(historico);

            return Ok(exitoso);
        }

        // DELETE: api/Historico/5
        [ResponseType(typeof(Historico))]
        public IHttpActionResult DeleteHistorico(Guid id)
        {
            Historico historico = db.Historicoes.Find(id);
            if (historico == null)
            {
                return NotFound();
            }

            db.Historicoes.Remove(historico);
            db.SaveChanges();

            return Ok(historico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HistoricoExists(Guid id)
        {
            return db.Historicoes.Count(e => e.id == id) > 0;
        }
    }
}