using ClientService.EF.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ClientService
{
    public interface IApplicationDbContext
    {
        DbSet<DbCustomer> Customers { get; set; }
    }
}