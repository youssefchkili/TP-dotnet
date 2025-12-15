using MyFirstApp.Models;

namespace MyFirstApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationdbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public IMovieRepository Movies { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IGenreRepository Genres { get; private set; }

        public UnitOfWork(ApplicationdbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
            
            Movies = new MovieRepository(_context);
            Customers = new CustomerRepository(_context);
            Genres = new GenreRepository(_context);
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GenericRepository<T>(_context);
            }
            return (IGenericRepository<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
