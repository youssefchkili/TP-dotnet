using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;

namespace MyFirstApp.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly ApplicationdbContext _context;

        public GenreRepository(ApplicationdbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllWithMoviesAsync()
        {
            return await _context.genres
                .Include(g => g.Movies)
                .ToListAsync();
        }

        public async Task<Genre?> GetByIdWithMoviesAsync(int id)
        {
            return await _context.genres
                .Include(g => g.Movies)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<object>> GetTop3PopularGenresAsync()
        {
            return await _context.genres
                .Select(g => new
                {
                    GenreName = g.Name,
                    MovieCount = g.Movies!.Count()
                })
                .OrderByDescending(g => g.MovieCount)
                .Take(3)
                .ToListAsync<object>();
        }
    }
}
