namespace MyFirstApp.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IMovieRepository Movies { get; }
        ICustomerRepository Customers { get; }
        IGenreRepository Genres { get; }
        IGenericRepository<T> Repository<T>() where T : class;
        
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
