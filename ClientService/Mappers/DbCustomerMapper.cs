using System;
using ClientService.EF.Data;
using ClientService.EF.DbModels;
using ClientService.Models.Requests;

namespace ClientService.Mappers
{
    public class DbCustomerMapper : IDbCustomerMapper
    {
        public DbCustomer Map(CreateCustomerRequest customerRequest)
        {
            return new DbCustomer
            {
                Login = customerRequest.Login,
                FirstName = customerRequest.FirstName,
                SecondName = customerRequest.SecondName,
                Email = customerRequest.Email,
                DateOfRegistration = DateTime.Now
            };
        }
    }
}