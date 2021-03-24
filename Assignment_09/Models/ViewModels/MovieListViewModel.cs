using System;
using System.Collections.Generic;

namespace Assignment_09.Models.ViewModels
{
    //This is the IEnumerable that is passed to the MovieList View page
    public class MovieListViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }
    }
}
