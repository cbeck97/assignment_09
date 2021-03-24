using System;
using System.Linq;

namespace Assignment_09.Models
{
    public class EFMovieRepository : IMovieRepository
    {
        private MovieDbContext _context;

        public EFMovieRepository(MovieDbContext context)
        {
            _context = context;
        }

        public IQueryable<Movie> Movies => _context.Movies;
    }
}
