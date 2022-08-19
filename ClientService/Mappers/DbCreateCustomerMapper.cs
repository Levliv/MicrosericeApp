using System;
using ClientService.EF.Data;
using ClientService.EF.DbModels;
using ClientService.Models.Requests;

namespace ClientService.Mappers
{
    public class DbCreateCustomerMapper : IDbCreateCustomerMapper
    {
        public DbCustomer Map(CreateCustomerRequest createCustomerRequest)
        {
            return new DbCustomer
            {
                Login = createCustomerRequest.Login,
                FirstName = createCustomerRequest.FirstName,
                SecondName = createCustomerRequest.SecondName,
                Email = createCustomerRequest.Email,
                DateOfRegistration = DateTime.Now
            };
        }
    }
}