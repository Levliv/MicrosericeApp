using ClientService.EF.Data.Interfaces;

namespace ClientService.EF.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        
        /// <summary>
        /// Customer ctor using data context.
        /// </summary>
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}