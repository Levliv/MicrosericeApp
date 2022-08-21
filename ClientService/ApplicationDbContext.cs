using ClientService.EF.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ClientService
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<DbCustomer> Customers { get; set; } = null!;
        public DbSet<DbBakedGood> BakedGoods { get; set; }
        public DbSet<DbOrder> Orders { get; set; }
        public DbSet<DbBakedGoodOrder> BakedGoodsOrders { get; set; }
    }
}