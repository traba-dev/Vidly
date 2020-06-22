using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MoviesFormViewModels
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public String Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime RealeaseDate { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "The field Number in Stock must be between 1 and 20")]
        [Display(Name = "Number in stock")]
        public int NumberInStock { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string GetTitle()
        {
            return (this.Id > 0) ? "Edit Movie" : "New Movie";
        }

        public MoviesFormViewModels()
        {
            this.Id = 0;
        }

        public MoviesFormViewModels(Movie movie)
        {
            this.Id = movie.Id;
            this.Name = movie.name;
            this.RealeaseDate = movie.RealeaseDate;
            this.Genre = "";
            this.GenreId = movie.GenreId;
            this.NumberInStock = movie.NumberInStock;
        }
    }
}