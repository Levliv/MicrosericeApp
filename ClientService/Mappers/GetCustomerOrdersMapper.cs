using System.Collections.Generic;
using ClientService.EF.DbModels;
using ClientService.Mappers.Interfaces;
using ClientService.Models.Responses;

namespace ClientService.Mappers
{
    public class GetCustomerOrdersMapper : IGetCustomerOrdersMapper
    {
        public GetCustomerOrdersResponse Map(DbCustomer dbCustomer, IEnumerable<GetOrderResponse> getOrderResponses)
        {
            return new GetCustomerOrdersResponse
            {
                Id = dbCustomer.Id,
                Login = dbCustomer.Login,
                FirstName = dbCustomer.FirstName,
                SecondName = dbCustomer.SecondName,
                Email = dbCustomer.Email,
                Orders = getOrderResponses
            };
        }
    }
}