using System;
using System.Linq;

namespace Assignment_09.Models
{
    public interface IMovieRepository
    {
        IQueryable<Movie> Movies { get; }
    }
}
