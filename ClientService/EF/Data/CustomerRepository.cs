using System;
using System.Linq;
using ClientService.EF.Data.Interfaces;
using ClientService.EF.DbModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        public Guid? Create(DbCustomer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            Guid createdUserGuid = _context.Customers.Where(x => x.Login == customer.Login).Select(x => x.Id).FirstOrDefault();
            return createdUserGuid;
        }

        /// <summary>
        /// Gets customer personal data.
        /// </summary>
        public DbCustomer? Read(Guid customerGuid)
        {
            return _context.Customers.Find(customerGuid);
        }

        /// <summary>
        /// Edits customer's data.
        /// </summary>
        /// <param name="customerToEditId"> Guid of the customer who's data will be updated. </param>
        /// <param name="customer"> New customer data. </param>
        /// <returns> If user to update was found: this user's Guid, otherwise: null. </returns>
        public Guid? Update(Guid customerToEditId, DbCustomer customer)
        {
            DbCustomer? oldCustomer = _context.Customers.Find(customerToEditId);
            if (oldCustomer is null)
            {
                return null;
            }
            EntityEntry<DbCustomer> customerToEdit = _context.Customers.Update(oldCustomer)!;
            customerToEdit.Entity.Login= customer.Login;
            customerToEdit.Entity.FirstName = customer.FirstName;
            customerToEdit.Entity.SecondName = customer.SecondName;
            _context.SaveChanges();
            return _context.Customers.Find(customerToEditId).Id;
        }

        /// <summary>
        /// Deletes chosen customer.
        /// </summary>
        /// <param name="customerToDeleteId"> Guid of the customer to delete. </param>
        /// <returns> If customer was found: Guid of the deleted customer, if not: null. </returns>
        public Guid? Delete(Guid customerToDeleteId)
        {
            DbCustomer? customerToDelete = _context.Customers.Find(customerToDeleteId);
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
            return _context.Customers.Where(customer => customer.Login == login).FirstOrDefault() != null;
        }
    }
}