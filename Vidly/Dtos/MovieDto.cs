using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public byte GenreId { get; set; }

        [Required]
        public DateTime RealeaseDate { get; set; }

        [Required]
        public DateTime DateAdd { get; set; }

        [Required]
        public int NumberInStock { get; set; }

        public GenreDto Genre { get; set; }
    }
}