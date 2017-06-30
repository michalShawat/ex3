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
    /// <summary>
    /// controller of the users
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class UsersController : ApiController
    {
        /// <summary>
        /// The database
        /// </summary>
        private MazeContext db = new MazeContext();



        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        // GET: api/Users/5
        [ResponseType(typeof(User))]
        [ActionName("GetUser")]
        [Route("api/Users/GetUser/{name}/{password}")]
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


        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns></returns>
        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users.OrderByDescending(m => m.wins - m.losses);
        }


        /// <summary>
        /// Puts the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
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

        // POST: api/Users
        /// <summary>
        /// Posts the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        //post = put for the first time, put = update
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


        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Users the exists.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.username == id) > 0;
        }


        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        string ComputeHash(string input)
        {
            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha.ComputeHash(buffer);
            string hash64 = Convert.ToBase64String(hash);
            return hash64;
        }

        /// <summary>
        /// Updates the user in winning.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        [ActionName("UpdateWin")]
        [Route("api/Users/UpdateWin/{name}")]
        [HttpGet()]
        public IHttpActionResult UpdateWin(string name)
        {
            User user = db.Users.Find(name);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.wins++;
            }
            db.SaveChanges();
            return Ok(user);
        }

        /// <summary>
        /// Updates the user in losing.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        [ActionName("UpdateLose")]
        [Route("api/Users/UpdateLose/{name}")]
        [HttpGet()]
        public IHttpActionResult UpdateLose(string name)
        {
            User user = db.Users.Find(name);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                user.losses++;
            }
            db.SaveChanges();
            return Ok(user);
        }

    }

}
