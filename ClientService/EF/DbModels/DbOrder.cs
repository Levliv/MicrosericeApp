using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientService.EF.DbModels
{
    public class DbOrder
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
    
        public ICollection<DbBakedGoodOrder> BakedGoodOrders { get; set; }
    
        public DbCustomer Customer { get; set; }

        public DbOrder()
        {
            BakedGoodOrders = new HashSet<DbBakedGoodOrder>();
            Customer = new();
        }
    }
    
    public class DbOrderConfiguration : IEntityTypeConfiguration<DbOrder>
    {
        public void Configure(EntityTypeBuilder<DbOrder> builder)
        {
            builder
                .ToTable("Orders");
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.OrderTime)
                .HasDefaultValue(DateTime.Now);

            builder
                .HasOne(orders => orders.Customer)
                .WithMany(customers => customers.Orders)
                .HasForeignKey(orders => orders.CustomerId);

            builder
                .HasMany(order => order.BakedGoodOrders)
                .WithOne(bakedGoodOrders => bakedGoodOrders.Order);
        }
    }
}