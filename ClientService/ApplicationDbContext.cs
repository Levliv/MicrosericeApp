using System.Data.Common;
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
    }
}