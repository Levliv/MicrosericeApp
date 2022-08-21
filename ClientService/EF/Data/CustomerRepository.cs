using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ClientService.EF.Data.Interfaces;
using ClientService.EF.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;

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
            return customer.Id;
        }


        public DbCustomer? Read2(string login)
        {
            var t =  _context.Customers
                .Include(customer => customer.Orders)
                .ThenInclude(orders => orders.BakedGoodOrders)
                .ThenInclude(bakedGoods => bakedGoods.BakedGood)
                .FirstOrDefault(customer => customer.Login == login);
            return t;
        }

        /// <summary>
        /// Gets customer personal data.
        /// </summary>
        public Tuple<DbCustomer, List<Tuple<DbOrder, List<Tuple<DbBakedGood, DbBakedGoodOrder>>>>>? Read(string login)
        {
            DbCustomer? dbCustomers = _context.Customers
                .Include(customer => customer.Orders)
                .ThenInclude(orders => orders.BakedGoodOrders)
                .ThenInclude(bakedGoods => bakedGoods.BakedGood)
                .FirstOrDefault(customer => customer.Login == login);
            
            if (dbCustomers is null)
            {
                return null;
            }
            
            List<DbOrder> dbOrders = dbCustomers.Orders.ToList();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Found: {dbOrders.Count}");
            List<Tuple<DbOrder, List<Tuple<DbBakedGood, DbBakedGoodOrder>>>> result2 = new ();
            foreach (DbOrder dbOrder in dbOrders)
            {
                List<DbBakedGoodOrder> bakedGoodOrders = dbOrder.BakedGoodOrders.ToList();
                List<Tuple<DbBakedGood, DbBakedGoodOrder>> result1 = new ();
                foreach (DbBakedGoodOrder bakedGoodOrder in bakedGoodOrders)
                {
                    var e = Tuple.Create(bakedGoodOrder.BakedGood, bakedGoodOrder);
                    result1.Add(e);
                }

                var i = Tuple.Create(dbOrder, result1);
                result2.Add(i);
            }
            return Tuple.Create(dbCustomers, result2);
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
        
        public Guid? Update2(Guid customerToEditId, DbCustomer customer)
        {
            DbCustomer? customerToEdit = _context.Customers.FirstOrDefault(x => x.Id == customerToEditId);
            if (customerToEdit is null)
            {
                return null;
            }
            customerToEdit.Login= customer.Login;
            customerToEdit.FirstName = customer.FirstName;
            customerToEdit.SecondName = customer.SecondName;
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
            return _context.Customers.Any(customer => customer.Login == login);
        }
        
    }
}