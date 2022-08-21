using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientService.EF.DbModels
{
    public class DbBakedGoodOrder
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid BakedGoodId { get; set; }
        public float ProductWeight { get; set; }
    
        public DbBakedGood BakedGood { get; set; }
    
        public DbOrder Order { get; set; }
        
        public DbBakedGoodOrder()
        {
            BakedGood = new();
            Order = new();
        }
    }
    
    public class DbBakedGoodOrderConfiguration : IEntityTypeConfiguration<DbBakedGoodOrder>
    {
        public void Configure(EntityTypeBuilder<DbBakedGoodOrder> builder)
        {
            builder
                .ToTable("bakedGoodsOrders");
        
            builder
                .HasKey(x => x.Id);
        
            builder
                .HasOne(bakedGoodsOrders => bakedGoodsOrders.BakedGood)
                .WithMany(bakedGood => bakedGood.BakedGoodsOrders)
                .HasForeignKey(bakedGoodsOrders => bakedGoodsOrders.BakedGoodId);
        
            builder
                .HasOne(bakedGoodsOrders => bakedGoodsOrders.Order)
                .WithMany(orders => orders.BakedGoodOrders)
                .HasForeignKey(bakedGoodsOrders => bakedGoodsOrders.OrderId);
        }
    }
}