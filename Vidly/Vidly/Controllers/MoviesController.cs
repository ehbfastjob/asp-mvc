using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ViewResult ShowMovies()
        {
            var movies = GetMovies();
           
            return View(movies);
        }

        public IEnumerable<Movie> GetMovies()
        {
            return  new List<Movie>
            {
                new Movie {Id = 1, Name = "Much Loved"},
                new Movie {Id = 2  ,Name = "Skyline"},
                new Movie {Id = 3, Name="Deadpole"}
            };
        }

    }
}