using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ViewResult ShowMovies()
        {

            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }


        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return  new HttpNotFoundResult();
            }

            return View(movie);
        }

        public ActionResult NewMovie(string s)
        {
            ViewBag.Header = "New Movie";
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel
            {
                //movie = new Movie(),
                Genres = genres
            };

            return View(viewModel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("NewMovie", viewModel);
            }
            ViewBag.Header = "Edit Movie";
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;

            }
            
            
                _context.SaveChanges();
            

            return RedirectToAction("ShowMovies", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                var movieViewModel = new MovieFormViewModel
                {
                    movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("NewMovie", movieViewModel);
            }
        }
    }
}