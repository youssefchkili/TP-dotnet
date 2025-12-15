using MyFirstApp.Models;

namespace MyFirstApp.Repositories
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetAllWithMoviesAsync();
        Task<Genre?> GetByIdWithMoviesAsync(int id);
        Task<IEnumerable<object>> GetTop3PopularGenresAsync();
    }
}
