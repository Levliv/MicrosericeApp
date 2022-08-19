using System;
using ClientService.EF.DbModels;

namespace ClientService.EF.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Guid? Create(DbCustomer customer);
        bool DoesSameLoginExist(string login);
    }
}