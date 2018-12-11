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
using FinalP10.Models;

namespace FinalP10.Controllers
{
    public class CLIENTEAPIController : ApiController
    {
        private Ciudad_DollarEntities1 db = new Ciudad_DollarEntities1();

        // GET: api/CLIENTEAPI
        public IQueryable<CLIENTE> GetCLIENTE()
        {
            return db.CLIENTE;
        }

        // GET: api/CLIENTEAPI/5
        [ResponseType(typeof(CLIENTE))]
        public IHttpActionResult GetCLIENTE(int id)
        {
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            if (cLIENTE == null)
            {
                return NotFound();
            }

            return Ok(cLIENTE);
        }

        // PUT: api/CLIENTEAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCLIENTE(int id, CLIENTE cLIENTE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cLIENTE.Id)
            {
                return BadRequest();
            }

            db.Entry(cLIENTE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CLIENTEExists(id))
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

        // POST: api/CLIENTEAPI
        [ResponseType(typeof(CLIENTE))]
        public IHttpActionResult PostCLIENTE(CLIENTE cLIENTE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CLIENTE.Add(cLIENTE);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cLIENTE.Id }, cLIENTE);
        }

        // DELETE: api/CLIENTEAPI/5
        [ResponseType(typeof(CLIENTE))]
        public IHttpActionResult DeleteCLIENTE(int id)
        {
            CLIENTE cLIENTE = db.CLIENTE.Find(id);
            if (cLIENTE == null)
            {
                return NotFound();
            }

            db.CLIENTE.Remove(cLIENTE);
            db.SaveChanges();

            return Ok(cLIENTE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CLIENTEExists(int id)
        {
            return db.CLIENTE.Count(e => e.Id == id) > 0;
        }
    }
}