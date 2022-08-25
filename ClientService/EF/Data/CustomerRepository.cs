using System;
using System.Linq;
using System.Threading.Tasks;
using ClientService.EF.Data.Interfaces;
using ClientService.EF.DbModels;
using ClientService.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace ClientService.EF.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Customer ctor using data context.
        /// </summary>
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="customer"> Customer to create. </param>
        public async Task<Guid?> CreateAsync(DbCustomer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }

        /// <summary>
        /// Gets customer personal data.
        /// </summary>
        public Task<DbCustomer> ReadAsync(string login)
        {
            return _context.Customers
                .Include(customer => customer.Orders)
                .ThenInclude(orders => orders.BakedGoodOrders)
                .ThenInclude(bakedGoods => bakedGoods.BakedGood)
                .FirstOrDefaultAsync(customer => customer.Login == login);
        }

        public async Task<DbCustomer> UpdateAsync(EditCustomerPersonalInfoRequest customerToEditRequest, DbCustomer customer)
        {
            DbCustomer customerToEdit = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerToEditRequest.CustomerId);
            if (customerToEdit is null)
            {
                return null;
            }
            customerToEdit.Login= customer.Login;
            customerToEdit.FirstName = customer.FirstName;
            customerToEdit.SecondName = customer.SecondName;
            await _context.SaveChangesAsync();
            return await _context.Customers.FindAsync(customerToEditRequest.CustomerId);
        }


        /// <summary>
        /// Deletes chosen customer.
        /// </summary>
        /// <param name="customerToDeleteId"> Guid of the customer to delete. </param>
        /// <returns> If customer was found: Guid of the deleted customer, if not: null. </returns>
        public Guid? Delete(Guid customerToDeleteId)
        {
            DbCustomer customerToDelete = _context.Customers.Find(customerToDeleteId);
            if (customerToDelete is null)
            {
                return null;
            }
            _context.Customers.Remove(customerToDelete);
            _context.SaveChanges();
            return customerToDeleteId;
        }

        public bool DoesSameLoginExist(string login)
        {
            return _context.Customers.Any(customer => customer.Login == login);
        }
        
    }
}