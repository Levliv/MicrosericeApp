using System;
using System.Collections.Generic;
using ClientService.EF.DbModels;

namespace ClientService.EF.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Guid? Create(DbCustomer customer);
        bool DoesSameLoginExist(string login);
        DbCustomer Read(string login);
    }
}