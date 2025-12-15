using MyFirstApp.Models;

namespace MyFirstApp.Repositories
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetAllWithGenreAsync();
        Task<Movie?> GetByIdWithGenreAsync(int id);
        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(int genreId);
        Task<IEnumerable<Movie>> GetMoviesInStockAsync();
        Task<IEnumerable<Movie>> GetActionMoviesInStockAsync();
    }
}
