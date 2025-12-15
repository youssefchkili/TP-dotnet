using System.Linq.Expressions;

namespace MyFirstApp.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // Récupérer tous les éléments
        Task<IEnumerable<T>> GetAllAsync();

        // Récupérer un élément par ID
        Task<T?> GetByIdAsync(int id);

        // Récupérer avec des critères de filtrage
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Ajouter un nouvel élément
        Task AddAsync(T entity);

        // Ajouter plusieurs éléments
        Task AddRangeAsync(IEnumerable<T> entities);

        // Mettre à jour un élément
        void Update(T entity);

        // Supprimer un élément
        void Remove(T entity);

        // Supprimer plusieurs éléments
        void RemoveRange(IEnumerable<T> entities);

        // Vérifier si un élément existe
        Task<bool> ExistsAsync(int id);

        // Compter les éléments
        Task<int> CountAsync();

        // Compter avec un filtre
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
