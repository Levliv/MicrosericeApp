using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientService.EF.DbModels
{
    public class DbBakedGood
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<DbBakedGoodOrder> BakedGoodsOrders {get; set; }
        
        public DbBakedGood()
        {
            BakedGoodsOrders = new HashSet<DbBakedGoodOrder>();
        }
    }
    
    public class DbBakedGoodConfiguration : IEntityTypeConfiguration<DbBakedGood>
    {
        public void Configure(EntityTypeBuilder<DbBakedGood> builder)
        {
            builder
                .ToTable("BakedGoods");
        
            builder
                .HasKey(x => x.Id);
        
            builder
                .HasAlternateKey(x => x.Name);

            builder
                .HasMany(bakedGood => bakedGood.BakedGoodsOrders)
                .WithOne(bakedGoodOrders => bakedGoodOrders.BakedGood);
        }
    }
}