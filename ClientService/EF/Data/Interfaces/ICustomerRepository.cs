using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClientService.EF.DbModels;
using ClientService.Models.Requests;

namespace ClientService.EF.Data.Interfaces
{
    public interface ICustomerRepository
    { 
        Task<Guid?> CreateAsync(DbCustomer customer);
        Task<DbCustomer> ReadAsync(string login);
        Task<DbCustomer> UpdateAsync(EditCustomerPersonalInfoRequest customerToEditRequest, DbCustomer customer);
        bool DoesSameLoginExist(string login);
    }
}