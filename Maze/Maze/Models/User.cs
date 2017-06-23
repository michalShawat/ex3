using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Maze.Models
{
    public class User
    {
        [Key]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public string email { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public object Recipient { get; internal set; }
    }
}