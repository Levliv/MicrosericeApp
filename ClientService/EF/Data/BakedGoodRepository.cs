using ClientService.EF.Data.Interfaces;

namespace ClientService.EF.Data
{
    public class BakedGoodRepository : IBakedGoodRepository
    {
        private readonly ApplicationDbContext _context;
        
        /// <summary>
        /// Customer ctor using data context.
        /// </summary>
        public BakedGoodRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}