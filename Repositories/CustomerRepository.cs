using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;

namespace MyFirstApp.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationdbContext _context;

        public CustomerRepository(ApplicationdbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllWithMembershipTypeAsync()
        {
            return await _context.customers
                .Include(c => c.MembershipType)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdWithMembershipTypeAsync(int id)
        {
            return await _context.customers
                .Include(c => c.MembershipType)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetSubscribedCustomersAsync()
        {
            return await _context.customers
                .Include(c => c.MembershipType)
                .Where(c => c.IsSubscribed)
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomersWithHighDiscountAsync(float discountRate)
        {
            return await _context.customers
                .Include(c => c.MembershipType)
                .Where(c => c.IsSubscribed && c.MembershipType!.DiscountRate > discountRate)
                .ToListAsync();
        }
    }
}
