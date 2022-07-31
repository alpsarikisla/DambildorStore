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
using HortumBank.Models;

namespace HortumBank.Controllers.API
{
    public class SanalPosMusterisController : ApiController
    {
        private HortumBankDBEntities db = new HortumBankDBEntities();

        // GET: api/SanalPosMusteris
        public IQueryable<SanalPosMusteri> GetSanalPosMusteri()
        {
            return db.SanalPosMusteri;
        }

        // GET: api/SanalPosMusteris/5
        [ResponseType(typeof(SanalPosMusteri))]
        public IHttpActionResult GetSanalPosMusteri(int id)
        {
            SanalPosMusteri sanalPosMusteri = db.SanalPosMusteri.Find(id);
            if (sanalPosMusteri == null)
            {
                return NotFound();
            }

            return Ok(sanalPosMusteri);
        }

        // PUT: api/SanalPosMusteris/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSanalPosMusteri(int id, SanalPosMusteri sanalPosMusteri)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sanalPosMusteri.ID)
            {
                return BadRequest();
            }

            db.Entry(sanalPosMusteri).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanalPosMusteriExists(id))
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

        // POST: api/SanalPosMusteris
        [ResponseType(typeof(SanalPosMusteri))]
        public IHttpActionResult PostSanalPosMusteri(SanalPosMusteri sanalPosMusteri)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SanalPosMusteri.Add(sanalPosMusteri);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sanalPosMusteri.ID }, sanalPosMusteri);
        }

        // DELETE: api/SanalPosMusteris/5
        [ResponseType(typeof(SanalPosMusteri))]
        public IHttpActionResult DeleteSanalPosMusteri(int id)
        {
            SanalPosMusteri sanalPosMusteri = db.SanalPosMusteri.Find(id);
            if (sanalPosMusteri == null)
            {
                return NotFound();
            }

            db.SanalPosMusteri.Remove(sanalPosMusteri);
            db.SaveChanges();

            return Ok(sanalPosMusteri);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SanalPosMusteriExists(int id)
        {
            return db.SanalPosMusteri.Count(e => e.ID == id) > 0;
        }
    }
}