using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private VidlyContext _context;

        public MoviesController()
        {
            _context = new VidlyContext();
        }


        // GET: Movies
        //public ActionResult Random()
        //{
        //    var movie = new Movie()
        //    {
        //        name = "Shrek!"
        //    };

        //    //Tipos de actionResult
        //    //return HttpNotFound();
        //    //return Content("test");
        //    //return EmptyResult();

        //    var customers = new List<Customer>
        //    {
        //        new Customer {Name = "Customer 1"},
        //        new Customer {Name = "Customer 2"}, 
        //        new Customer {Name = "Customer 3"},
        //        new Customer {Name = "Customer 4"}, 
        //        new Customer {Name = "Customer 5"},
        //        new Customer {Name = "Customer 6"}
        //    };

        //    var randomMovieViewModel = new RandomMovieViewModels()
        //    {
        //        Movie = movie,
        //        customers = customers
        //    };




        //    return View(randomMovieViewModel);
        //}

        //[Route("movies/release/{year}/{month}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}

        public ActionResult Index()
        {
            List<Movie> movies = _context.Movies.Include(c => c.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            List<Movie> movies = _context.Movies.Include(c => c.Genre).ToList();

            Movie movie = movies.Find(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        public ActionResult NewMovie()
        {

            List<Genre> genres = _context.Genres.ToList();

            MoviesFormViewModels viewModels = new MoviesFormViewModels()
            {
                Genres = genres
            };

            return View("NewMovie", viewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMovie(MoviesFormViewModels pMovie)
        {
            Movie movie = new Movie()
            {
                Id = pMovie.Id,
                name = pMovie.Name,
                RealeaseDate = pMovie.RealeaseDate,
                GenreId = pMovie.GenreId,
                NumberInStock = pMovie.NumberInStock
            };


            if (movie.Id == 0)
            {
                movie.DateAdd = DateTime.Now;
                _context.Movies.Add(movie);
            } else
            {
                Movie _Movie = _context.Movies.SingleOrDefault(m => m.Id == movie.Id);
                _Movie.name = movie.name;
                _Movie.GenreId = movie.GenreId;
                _Movie.RealeaseDate = movie.RealeaseDate;
                _Movie.NumberInStock = movie.NumberInStock;

                _context.Entry(_Movie).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Movies");
        }
        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();


            MoviesFormViewModels viewModels = new MoviesFormViewModels(movie)
            {
                Genres = _context.Genres.ToList()
            };


            return View("NewMovie", viewModels);
        }
    }
}