using System;
using System.Collections.Generic;
using ClientService.EF.DbModels;

namespace ClientService.EF.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Guid? Create(DbCustomer customer);
        Tuple<DbCustomer, List<Tuple<DbOrder, List<Tuple<DbBakedGood, DbBakedGoodOrder>>>>>? Read(string login);
        bool DoesSameLoginExist(string login);
        DbCustomer? Read2(string login);
    }
}