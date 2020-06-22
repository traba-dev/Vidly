using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        
        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        [Required]
        public DateTime RealeaseDate{ get; set; }

        [Required]
        public DateTime DateAdd { get; set; }

        [Required]
        public int NumberInStock { get; set; }
    }
}