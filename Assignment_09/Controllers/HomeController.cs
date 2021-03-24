using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Assignment_09.Models;
using Assignment_09.Models.ViewModels;

namespace Assignment_09.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MovieDbContext Context { get; }

        public HomeController(ILogger<HomeController> logger, MovieDbContext context)
        {
            _logger = logger;
            Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Podcasts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        //Adds the movie to the database and returns the movieList view
        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                Context.Movies.Add(movie);
                Context.SaveChanges();
            }
            //Returns the MovieList View with an IEnumerable that contains all of the movies in
            //the database
            return View("MovieList", new MovieListViewModel()
            {
                Movies = Context.Movies
                    .Select(s => s)
            });
        }

        //Returns the MovieList View with an IEnumerable that contains all of the movies in
        //the database
        [HttpGet]
        public IActionResult MovieList()
        {
            return View(new MovieListViewModel()
            {
                Movies = Context.Movies
                    .Select(s => s)
            });
        }

        //This gets called when the user edits a movie. The correct movie
        //is identified by the id and passed to the edit movie view
        [HttpPost]
        public IActionResult MovieList(int id)
        {
            Movie movie = Context.Movies.Where(s => s.MovieID == id).FirstOrDefault();

            ViewData["id"] = id;

            return View("EditMovie", movie);
        }

        //This saves the changes to the movie and then returns to the movie list view
        [HttpPost]
        public IActionResult EditMovie(Movie mov, int id)
        {
            Movie originalMovie = Context.Movies.Where(s => s.MovieID == id).FirstOrDefault();
            int ID = id;

            originalMovie.Category = mov.Category;
            originalMovie.Title = mov.Title;
            originalMovie.Year = mov.Year;
            originalMovie.Director = mov.Director;
            originalMovie.Rating = mov.Rating;
            originalMovie.Edited = mov.Edited;
            originalMovie.Lent = mov.Lent;
            originalMovie.Notes = mov.Notes;

            Context.Update(originalMovie);
            Context.SaveChanges();

            return View("MovieList",new MovieListViewModel()
            {
                Movies = Context.Movies
                    .Select(s => s)
            });
        }

        //This identifies the movie that needs to be deleted by its id
        //and then deletes that movie from the database
        public IActionResult DeleteMovie(int id)
        {
            Movie movie = Context.Movies.Where(s => s.MovieID == id).FirstOrDefault();

            Context.Movies.Remove(movie);
            Context.SaveChanges();

            return View("MovieList", new MovieListViewModel()
            {
                Movies = Context.Movies
                    .Select(s => s)
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
