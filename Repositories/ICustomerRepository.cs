using MyFirstApp.Models;

namespace MyFirstApp.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllWithMembershipTypeAsync();
        Task<Customer?> GetByIdWithMembershipTypeAsync(int id);
        Task<IEnumerable<Customer>> GetSubscribedCustomersAsync();
        Task<IEnumerable<Customer>> GetCustomersWithHighDiscountAsync(float discountRate);
    }
}
