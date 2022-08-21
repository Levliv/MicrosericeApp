using ClientService.EF.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ClientService
{
    public interface IApplicationDbContext
    {
        DbSet<DbCustomer> Customers { get; set; }
        DbSet<DbBakedGood> BakedGoods { get; set; }
        DbSet<DbOrder> Orders { get; set; }
        DbSet<DbBakedGoodOrder> BakedGoodsOrders { get; set; }
    }
}