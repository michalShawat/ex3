using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using Maze.Models;

namespace Maze.Controllers
{
    public class UsersController : ApiController
    {
        private MazeContext db = new MazeContext();


        // GET: api/Users/5
        [ResponseType(typeof(User))]
        [ActionName("GetUser")]
        //[Route("api/Users/GetUser/{name}/{password}")]
        public IHttpActionResult GetUser(string name, string password)
        {
            User user = db.Users.Find(name);
  
            if (user == null)
            {
                return NotFound();
            }
            else if (user.password == ComputeHash(password))
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
            
        }

        // GET: api/Users/GetUsers
        [ActionName("GetUsers")]
        [Route("api/Users")]
        [HttpGet()]
        public IQueryable<User> GetUsers()
        {
            return db.Users.OrderByDescending(m => m.wins - m.losses);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.username)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        //post = put for the first time, put = update
        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.password = ComputeHash(user.password);

            db.Users.Add(user);
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.username }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.username == id) > 0;
        }


        string ComputeHash(string input)
        {
            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha.ComputeHash(buffer);
            string hash64 = Convert.ToBase64String(hash);
            return hash64;
        }

        [ResponseType(typeof(User))]
        [ActionName("UpdateUser")]
        [Route("api/Users/UpdateUser/{name}/{flag}")]
        [HttpGet()]
        public IHttpActionResult UpdateUser(string name, int flag)
        {
            User user = db.Users.Find(name);

            if (user == null)
            {
                return NotFound();
            }
            else if (flag==1)
            {
                user.wins++;
            }
            else if(flag==0)
            {
                user.losses++;
            }
            db.SaveChanges();
            return Ok(user);
        }

    }

}

