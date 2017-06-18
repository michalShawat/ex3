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
using Maze.Models;

namespace Maze.Controllers
{
    public class MessangersController : ApiController
    {
        private MazeContext db = new MazeContext();

        // GET: api/Messangers
        public IQueryable<Messanger> GetMessangers()
        {
            return db.Messangers;
        }

        // GET: api/Messangers/5
        [ResponseType(typeof(Messanger))]
        public IHttpActionResult GetMessanger(string id)
        {
            Messanger messanger = db.Messangers.Find(id);
            if (messanger == null)
            {
                return NotFound();
            }

            return Ok(messanger);
        }

        // PUT: api/Messangers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessanger(string id, Messanger messanger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != messanger.username)
            {
                return BadRequest();
            }

            db.Entry(messanger).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessangerExists(id))
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

        // POST: api/Messangers
        [ResponseType(typeof(Messanger))]
        public IHttpActionResult PostMessanger(Messanger messanger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messangers.Add(messanger);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MessangerExists(messanger.username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = messanger.username }, messanger);
        }

        // DELETE: api/Messangers/5
        [ResponseType(typeof(Messanger))]
        public IHttpActionResult DeleteMessanger(string id)
        {
            Messanger messanger = db.Messangers.Find(id);
            if (messanger == null)
            {
                return NotFound();
            }

            db.Messangers.Remove(messanger);
            db.SaveChanges();

            return Ok(messanger);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessangerExists(string id)
        {
            return db.Messangers.Count(e => e.username == id) > 0;
        }
    }
}