using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Maze.Models
{
    /// <summary>
    /// model of the user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Key]
        public string username { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string password { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string email { get; set; }
        /// <summary>
        /// Gets or sets the wins.
        /// </summary>
        /// <value>
        /// The wins.
        /// </value>
        public int wins { get; set; }
        /// <summary>
        /// Gets or sets the losses.
        /// </summary>
        /// <value>
        /// The losses.
        /// </value>
        public int losses { get; set; }
        /// <summary>
        /// Gets the recipient.
        /// </summary>
        /// <value>
        /// The recipient.
        /// </value>
        public object Recipient { get; internal set; }
    }
}