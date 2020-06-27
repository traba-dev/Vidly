using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using System.Data.Entity;
using AutoMapper;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    public class MovieController : ApiController
    {

        VidlyContext _Context;

        public MovieController()
        {
            _Context = new VidlyContext();
        }

        [HttpGet]
        public IHttpActionResult GetMovies()
        {
            IEnumerable<Movie> movies = _Context.Movies.Include(c => c.Genre).ToList();

            return Ok(movies);
        }
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _Context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            movieDto.DateAdd = DateTime.Now;
            _Context.Movies.Add(Mapper.Map<MovieDto,Movie>(movieDto));
            _Context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + Mapper.Map<MovieDto, Movie>(movieDto).Id), movieDto);

        }
        [HttpPut]
        public IHttpActionResult UpdateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Movie movie = _Context.Movies.SingleOrDefault(m => m.Id == movieDto.Id);

            if (movie == null)
                return NotFound();

            Mapper.Map(movieDto,movie);

            _Context.Entry(movie).State = EntityState.Modified;
            _Context.SaveChanges();

            return Ok(movie);
        }
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = _Context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            _Context.Movies.Remove(movie);
            _Context.SaveChanges();

            return Ok(movie);
        }
    }
}
