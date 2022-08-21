using System;
using System.Collections.Generic;
using ClientService.EF.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientService.EF.DbModels
{
    public class DbCustomer
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? SecondName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfRegistration { get; set; }
        public bool IsActive { get; set; }
        public ICollection<DbOrder> Orders { get; set; }

        public DbCustomer()
        {
            Orders = new HashSet<DbOrder>();
        }
    }
}

public class DbCustomerConfiguration : IEntityTypeConfiguration<DbCustomer>
{
    public void Configure(EntityTypeBuilder<DbCustomer> builder)
    {
        builder
            .ToTable("Customers");

        builder
            .HasKey(x => x.Id);

        builder
            .HasMany(customer => customer.Orders)
            .WithOne(order => order.Customer);
    }
}