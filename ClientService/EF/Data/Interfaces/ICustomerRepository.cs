using System;
using ClientService.EF.DbModels;

namespace ClientService.EF.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Guid? Create(DbCustomer customer);
        DbCustomer? Read(string login);
        bool DoesSameLoginExist(string login);
    }
}