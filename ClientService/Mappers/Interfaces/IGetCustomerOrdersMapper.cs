using System.Collections.Generic;
using ClientService.EF.DbModels;
using ClientService.Models.Responses;

namespace ClientService.Mappers.Interfaces
{
    public interface IGetCustomerOrdersMapper
    {
        GetCustomerOrdersResponse Map(DbCustomer dbCustomer, IEnumerable<GetOrderResponse> getOrderResponses);
    }
}