using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;

namespace MyFirstApp.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly ApplicationdbContext _context;

        public MovieRepository(ApplicationdbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllWithGenreAsync()
        {
            return await _context.movies!
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public async Task<Movie?> GetByIdWithGenreAsync(int id)
        {
            return await _context.movies!
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreId)
        {
            return await _context.movies!
                .Include(m => m.Genre)
                .Where(m => m.GenreId == genreId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesInStockAsync()
        {
            return await _context.movies!
                .Include(m => m.Genre)
                .Where(m => m.Stock > 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetActionMoviesInStockAsync()
        {
            return await _context.movies!
                .Include(m => m.Genre)
                .Where(m => m.Genre!.Name == "Action" && m.Stock > 0)
                .ToListAsync();
        }
    }
}
