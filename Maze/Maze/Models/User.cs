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
        [Required]
        public string email { get; set; }
        [Required]
        public int wins { get; set; }
        [Required]
        public int losses { get; set; }


    }
}